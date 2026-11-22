using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReplicaLibrary.Converters;
using ReplicaLibrary.Models;

namespace ReplicaLibrary.Contexts;

public partial class ReplicaDbContext : DbContext
{
    public ReplicaDbContext()
    {
    }

    public ReplicaDbContext(DbContextOptions<ReplicaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatType> ChatTypes { get; set; }

    public virtual DbSet<GroupChat> GroupChats { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserChat> UserChats { get; set; }

    public virtual DbSet<UsersDeletedMessage> UsersDeletedMessages { get; set; }

    public virtual DbSet<UsersReadMessage> UsersReadMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=replica_db;User=root;Password=;");

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Guid>()
            .HaveConversion<TypeConverter>();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PRIMARY");

            entity.ToTable("chats");

            entity.HasIndex(e => e.ChatTypeId, "fk_chats_chat_types1_idx");

            entity.Property(e => e.ChatId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("chat_id");
            entity.Property(e => e.ChatTypeId).HasColumnName("chat_type_id");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("created_date_time");
            entity.Property(e => e.EditedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("edited_date_time");

            entity.HasOne(d => d.ChatType).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ChatTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_chats_chat_types1");
        });

        modelBuilder.Entity<ChatType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PRIMARY");

            entity.ToTable("chat_types");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<GroupChat>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.ChatId }).HasName("PRIMARY");

            entity.ToTable("group_chat");

            entity.HasIndex(e => e.ChatId, "fk_group_chat_chats1_idx");

            entity.Property(e => e.GroupId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("group_id");
            entity.Property(e => e.ChatId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("chat_id");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");

            entity.HasOne(d => d.Chat).WithMany(p => p.GroupChats)
                .HasForeignKey(d => d.ChatId)
                .HasConstraintName("fk_group_chat_chats1");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PRIMARY");

            entity.ToTable("messages");

            entity.HasIndex(e => e.ChatId, "fk_messages_chats1_idx");

            entity.HasIndex(e => e.SenderId, "fk_messages_users1_idx");

            entity.Property(e => e.MessageId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("message_id");
            entity.Property(e => e.ChatId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("chat_id");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .HasColumnName("content");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("created_date_time");
            entity.Property(e => e.EditedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("edited_date_time");
            entity.Property(e => e.SenderId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("sender_id");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .HasConstraintName("fk_messages_chats1");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_messages_users1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Login, "login_UNIQUE").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_id");
            entity.Property(e => e.BirthdayDate)
                .HasColumnType("date")
                .HasColumnName("birthday_date");
            entity.Property(e => e.DeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_date_time");
            entity.Property(e => e.EditedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("edited_date_time");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .HasColumnName("email");
            entity.Property(e => e.LastActionDateTime)
                .HasColumnType("datetime")
                .HasColumnName("last_action_date_time");
            entity.Property(e => e.Login)
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("password_hash");
            entity.Property(e => e.ProfileDescription)
                .HasMaxLength(300)
                .HasColumnName("profile_description");
            entity.Property(e => e.RegistrationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("registration_date_time");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Inactive'")
                .HasColumnType("enum('Active','Inactive')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<UserChat>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ChatId }).HasName("PRIMARY");

            entity.ToTable("user_chat");

            entity.HasIndex(e => e.ChatId, "fk_user_chat_chats1_idx");

            entity.HasIndex(e => e.RoleId, "fk_user_chat_roles1_idx");

            entity.Property(e => e.UserId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_id");
            entity.Property(e => e.ChatId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("chat_id");
            entity.Property(e => e.ClearedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("cleared_date_time");
            entity.Property(e => e.DeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_date_time");
            entity.Property(e => e.JoinedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("joined_date_time");
            entity.Property(e => e.LeftDateTime)
                .HasColumnType("datetime")
                .HasColumnName("left_date_time");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Chat).WithMany(p => p.UserChats)
                .HasForeignKey(d => d.ChatId)
                .HasConstraintName("fk_user_chat_chats1");

            entity.HasOne(d => d.Role).WithMany(p => p.UserChats)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_chat_roles1");

            entity.HasOne(d => d.User).WithMany(p => p.UserChats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_chat_users1");
        });

        modelBuilder.Entity<UsersDeletedMessage>(entity =>
        {
            entity.HasKey(e => new { e.UsersUserId, e.MessagesMessageId }).HasName("PRIMARY");

            entity.ToTable("users_deleted_messages");

            entity.HasIndex(e => e.MessagesMessageId, "fk_users_has_messages_messages1_idx");

            entity.HasIndex(e => e.UsersUserId, "fk_users_has_messages_users1_idx");

            entity.Property(e => e.UsersUserId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("users_user_id");
            entity.Property(e => e.MessagesMessageId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("messages_message_id");
            entity.Property(e => e.DeletedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_date_time");

            entity.HasOne(d => d.MessagesMessage).WithMany(p => p.UsersDeletedMessages)
                .HasForeignKey(d => d.MessagesMessageId)
                .HasConstraintName("fk_users_has_messages_messages1");

            entity.HasOne(d => d.UsersUser).WithMany(p => p.UsersDeletedMessages)
                .HasForeignKey(d => d.UsersUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_has_messages_users1");
        });

        modelBuilder.Entity<UsersReadMessage>(entity =>
        {
            entity.HasKey(e => new { e.MessageId, e.UserId }).HasName("PRIMARY");

            entity.ToTable("users_read_messages");

            entity.HasIndex(e => e.MessageId, "fk_users_read_messages_messages1_idx");

            entity.HasIndex(e => e.UserId, "fk_users_read_messages_users1_idx");

            entity.Property(e => e.MessageId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("message_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("user_id");
            entity.Property(e => e.ReadDateTime)
                .HasColumnType("datetime")
                .HasColumnName("read_date_time");

            entity.HasOne(d => d.Message).WithMany(p => p.UsersReadMessages)
                .HasForeignKey(d => d.MessageId)
                .HasConstraintName("fk_users_read_messages_messages1");

            entity.HasOne(d => d.User).WithMany(p => p.UsersReadMessages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_read_messages_users1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
