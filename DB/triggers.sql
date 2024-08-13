CREATE
OR REPLACE FUNCTION SET_HISTORY_ON_INSERT () RETURNS TRIGGER AS $$
BEGIN
  IF NEW."History" IS NULL OR NEW."History" = '' THEN
    NEW."History" := NEW."Id" || ';';
  END IF;
  RETURN NEW;
END; $$ LANGUAGE PLPGSQL;

CREATE TRIGGER HIST_TRIGGER_INSERT BEFORE INSERT ON "CurrentStock" FOR EACH ROW
EXECUTE PROCEDURE SET_HISTORY_ON_INSERT ();

CREATE
OR REPLACE FUNCTION SET_ARCHIVE_ON_DELETE () RETURNS TRIGGER AS $$
BEGIN 
  IF NOT(EXISTS(SELECT 1 FROM "ArchivedStock" WHERE "Id" = OLD."Id"))
	THEN
	  INSERT INTO "ArchivedStock" ("Id", "Worker", "Week", "Lab", "Location", "Recipients", "Ppr", "Category", "Phase", "Health", "History", "Remarks", "PlantCode", "MediumId", "Reason")
		VALUES (
	    OLD."Id",
	    OLD."Worker",
	    OLD."Week",
	    OLD."Lab",
	    OLD."Location",
	    OLD."Recipients",
	    OLD."Ppr",
	    OLD."Category",
	    OLD."Phase",
	    OLD."Health",
	    OLD."History",
	    OLD."Remarks",
	    OLD."PlantCode",
	    OLD."MediumId",
	    'No reason specified');
  END IF;
  RETURN NEW;
END; $$ LANGUAGE PLPGSQL;

CREATE TRIGGER ARCHIVE_TRIGGER_INSERT_ON_DELETE
AFTER DELETE ON "CurrentStock" FOR EACH ROW
EXECUTE PROCEDURE SET_ARCHIVE_ON_DELETE ();