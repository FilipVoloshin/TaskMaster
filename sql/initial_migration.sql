CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;

CREATE TABLE users (
    id uuid NOT NULL,
    name character varying(125) NOT NULL,
    CONSTRAINT pk_users PRIMARY KEY (id)
);

CREATE TABLE task_lists (
    id uuid NOT NULL,
    name character varying(255) NOT NULL,
    author_id uuid NOT NULL,
    created_at_utc timestamp with time zone NOT NULL,
    CONSTRAINT pk_task_lists PRIMARY KEY (id),
    CONSTRAINT fk_task_lists_users_author_id FOREIGN KEY (author_id) REFERENCES users (id) ON DELETE CASCADE
);

CREATE TABLE assigned_task_lists (
    user_id uuid NOT NULL,
    task_list_id uuid NOT NULL,
    author_id uuid NOT NULL,
    id uuid NOT NULL,
    CONSTRAINT pk_assigned_task_lists PRIMARY KEY (user_id, task_list_id),
    CONSTRAINT fk_assigned_task_lists_task_lists_task_list_id FOREIGN KEY (task_list_id) REFERENCES task_lists (id) ON DELETE CASCADE,
    CONSTRAINT fk_assigned_task_lists_users_author_id FOREIGN KEY (author_id) REFERENCES users (id) ON DELETE CASCADE,
    CONSTRAINT fk_assigned_task_lists_users_user_id FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
);

CREATE INDEX ix_assigned_task_lists_author_id ON assigned_task_lists (author_id);

CREATE INDEX ix_assigned_task_lists_task_list_id ON assigned_task_lists (task_list_id);

CREATE INDEX ix_task_lists_author_id ON task_lists (author_id);

CREATE INDEX ix_task_lists_created_at_utc ON task_lists (created_at_utc);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20230401125551_InitialMigration', '7.0.4');

COMMIT;

