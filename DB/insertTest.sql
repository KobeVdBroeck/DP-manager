INSERT INTO
	"CurrentStock" (
		"Worker",
		"Week",
		"Lab",
		"Location",
		"Recipients",
		"Ppr",
		"Category",
		"Phase",
		"Health",
		"History",
		"Remarks",
		"PlantCode",
		"MediumId"
	)
VALUES
	(
		'a',
		'1212',
		'abc',
		1,
		1,
		1,
		1,
		1,
		1,
		'',
		'',
		'0DP',
		1125
	);

DELETE FROM "CurrentStock"
WHERE
	"Id" = 2;