CREATE TABLE Users (
    id BIGINT IDENTITY PRIMARY KEY,
    username VARCHAR(255) NOT NULL UNIQUE,
    email VARCHAR(255) NOT NULL UNIQUE,
    is_admin BIT NOT NULL DEFAULT 0,
    password VARCHAR(255) NOT NULL,
);

CREATE TABLE Games (
    id BIGINT IDENTITY PRIMARY KEY,
    gamename VARCHAR(255) NOT NULL UNIQUE,
);

CREATE TABLE Scores (
    id BIGINT IDENTITY PRIMARY KEY,
    user_id BIGINT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    game_id BIGINT NOT NULL REFERENCES games(id) ON DELETE CASCADE,
    score BIGINT NOT NULL,
);

insert into Users values('admin','admin@1234.com',1,'admin@1234');