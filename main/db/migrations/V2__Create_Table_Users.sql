CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    user_name VARCHAR(50) NOT NULL DEFAULT '',
    password VARCHAR(130) NOT NULL DEFAULT '',
    full_name VARCHAR(120) NOT NULL,
    refresh_token VARCHAR(500) DEFAULT '',
    rf_expires TIMESTAMP,
    CONSTRAINT unique_user_name UNIQUE (user_name)
);
