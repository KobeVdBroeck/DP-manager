
SELECT *, 'Not archived' FROM "Stock"."CurrentStock" UNION ALL 
SELECT * FROM "Stock"."ArchivedStock" ;