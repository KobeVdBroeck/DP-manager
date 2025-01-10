CREATE EXTENSION IF NOT EXISTS pg_trgm; 

DROP INDEX IF EXISTS archive_history_index;
DROP INDEX IF EXISTS stock_history_index;

CREATE INDEX archive_history_index ON "ArchivedStock"("History");
CREATE INDEX stock_history_index ON "CurrentStock";