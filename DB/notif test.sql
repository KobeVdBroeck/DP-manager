DO $$
DECLARE
    interval_seconds INTEGER := 5; -- Interval in seconden
    iterations INTEGER := 10; -- Aantal herhalingen
BEGIN
    FOR i IN 1..iterations LOOP
        INSERT INTO "StockToProcess"
        VALUES (DEFAULT, 1, NOW()  + (i * interval_seconds) * INTERVAL '1 second');
    END LOOP;
END $$;
