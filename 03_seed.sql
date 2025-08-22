INSERT INTO Statuses(Name, RequireStatusDate) 
	VALUES('Pending Acceptance', 0), ('Accepted', 0), ('Picked Up', 1);

INSERT INTO ZipCodes(Zip) VALUES ('33101'), ('33102');

INSERT INTO Buyers(Name) VALUES ('Buyer ABC'), ('Buyer XYZ');

INSERT INTO Makes(Name) VALUES ('Toyota');

INSERT INTO Models(MakeId, Name) VALUES (1, 'Corolla');

INSERT INTO SubModels(ModelId, Name) VALUES (1, 'LE');

INSERT INTO Cars([Year], MakeId, ModelId, SubModelId) VALUES (2015,1,1,1);

INSERT INTO Customers(Name, Email) VALUES ('John Parker', 'john@parker.com');

INSERT INTO BuyerZipQuotes(BuyerId, Zip, Amount, IsActive) VALUES (1, '33101', 500, 1), (2, '33101', 540, 1);