CREATE EXTENSION IF NOT EXISTS pg_trgm; 

DROP INDEX IF EXISTS stock_history_index;
DROP INDEX IF EXISTS medium_id_index;
DROP INDEX IF EXISTS plant_code_index;

CREATE INDEX stock_history_index ON "ArchivedStock" USING gist ("History" gist_trgm_ops);
CREATE INDEX medium_id_index ON "Medium" USING HASH ("Id");
CREATE INDEX plant_code_index ON "Plant" USING HASH ("Code");