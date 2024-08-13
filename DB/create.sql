-- This script was generated by the ERD tool in pgAdmin 4.
-- Please log an issue at https://github.com/pgadmin-org/pgadmin4/issues/new/choose if you find any bugs, including reproduction steps.
BEGIN;


ALTER TABLE IF EXISTS "CurrentStock" DROP CONSTRAINT IF EXISTS None;

ALTER TABLE IF EXISTS "ArchivedStock" DROP CONSTRAINT IF EXISTS None;

DROP TRIGGER IF EXISTS HIST_TRIGGER_INSERT ON "CurrentStock";
DROP TRIGGER IF EXISTS ARCHIVE_TRIGGER_INSERT_ON_DELETE ON "CurrentStock";
DROP TABLE IF EXISTS "ArchivedStock";
DROP TABLE IF EXISTS "CurrentStock";
DROP TABLE IF EXISTS "Plant";
DROP TABLE IF EXISTS "Medium";

CREATE TABLE IF NOT EXISTS "CurrentStock"
(
    "Id" integer UNIQUE GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "Worker" character varying(5) DEFAULT null,
    "Week" character varying(4) NOT NULL,
    "Lab" character varying(10) NOT NULL,
    "Location" character varying(20) DEFAULT null,
    "Recipients" integer,
    "Ppr" integer NOT NULL DEFAULT 1,
    "Category" integer,
    "Phase" integer,
    "Health" integer,
    "History" text NOT NULL,
    "Remarks" text,
    "PlantCode" character varying(10) NOT NULL,
    "MediumId" integer NOT NULL,
    PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "ArchivedStock"
(
    "Id" integer UNIQUE GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    "Worker" character varying(5) DEFAULT null,
    "Week" character varying(4) NOT NULL,
    "Lab" character varying(10) NOT NULL,
    "Location" character varying(20) DEFAULT null,
    "Recipients" integer,
    "Ppr" integer NOT NULL DEFAULT 1,
    "Category" integer,
    "Phase" integer,
    "Health" integer,
    "History" text NOT NULL,
    "Remarks" text,
    "PlantCode" character varying(10) NOT NULL,
    "MediumId" integer NOT NULL,
    "Reason" character varying(20) NOT NULL,
    PRIMARY KEY ("Id")
) ;

COMMENT ON COLUMN "CurrentStock"."Worker"
    IS 'Null = extern';

COMMENT ON COLUMN "CurrentStock"."Ppr"
    IS 'Plants Per Recipient';

CREATE TABLE IF NOT EXISTS "Plant"
(
    "Code" character varying(10) NOT NULL,
    PRIMARY KEY ("Code")
);

CREATE TABLE IF NOT EXISTS "Medium"
(
    "Id" integer,
    "Description" text,
    PRIMARY KEY ("Id")
);

ALTER TABLE IF EXISTS "CurrentStock"
    ADD FOREIGN KEY ("PlantCode")
    REFERENCES "Plant" ("Code") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;

ALTER TABLE IF EXISTS "CurrentStock"
    ADD FOREIGN KEY ("MediumId")
    REFERENCES "Medium" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;

END;

