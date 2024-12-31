BEGIN TRANSACTION;

CREATE TABLE Partners(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	FirstName NVARCHAR(255) NOT NULL CHECK (LEN(FirstName) >= 2),
	LastName NVARCHAR(255) NOT NULL CHECK (LEN(LastName) >= 2),
	Address NVARCHAR(MAX) NULL,
	PartnerNumber NVARCHAR(20) NOT NULL CHECK ((LEN(PartnerNumber) = 20) AND (PartnerNumber NOT LIKE '%[^0-9]%')),
	CroatianPIN NVARCHAR(11) NULL CHECK (LEN(CroatianPIN) = 11 OR CroatianPIN IS NULL),
	PartnerTypeId INT  NOT NULL CHECK ((PartnerTypeId) IN (1, 2)),
	CreatedAtUtc DATETIME NOT NULL DEFAULT GETUTCDATE(),
	CreatedByUser NVARCHAR(255) NOT NULL CHECK (CreatedByUser LIKE '%_@__%.__%'),
	IsForeign BIT NOT NULL,
	ExternalCode NVARCHAR(20) NOT NULL UNIQUE CHECK (LEN(ExternalCode) BETWEEN 10 AND 20),
	Gender INT NOT NULL CHECK ((Gender) IN (1, 2, 3))
);

CREATE TABLE Policies(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	PolicyNumber NVARCHAR(15) NOT NULL CHECK (LEN(PolicyNumber) BETWEEN 10 AND 15),
	PolicyAmount DECIMAL(20, 2) NOT NULL,
	PartnerId INT NOT NULL,
	CONSTRAINT FK_Policies_Partners FOREIGN KEY (PartnerId) REFERENCES Partners(Id)
);

COMMIT TRANSACTION;