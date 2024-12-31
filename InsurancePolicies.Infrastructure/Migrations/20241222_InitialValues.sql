INSERT INTO Partners (FirstName, LastName, Address, PartnerNumber, CroatianPIN, PartnerTypeId, CreatedByUser, IsForeign, ExternalCode, Gender)
VALUES
('John', 'Doe', '123 Main St, Zagreb', '12345678901234567890', '12345678901', 1, 'admin@example.com', 0, 'EXTERNALCODE01', 1),
('Jane', 'Smith', '456 Elm St, London', '09876543210987654321', NULL, 1, 'jane.smith@example.com', 1, 'EXTERNALCODE02', 2),
('Acme Corp', 'Headquarters', '789 Pine St, Zagreb', '11223344556677889900', '98765432100', 2, 'admin@acme.com', 0, 'EXTERNALCODE03', 3),
('Globex', 'Global HQ', '101 International Blvd, Berlin', '99887766554433221100', NULL, 2, 'info@globex.com', 1, 'EXTERNALCODE04', 2);


DECLARE @JohnDoeId INT = (SELECT Id FROM Partners WHERE FirstName = 'John' AND LastName = 'Doe');
DECLARE @JaneSmithId INT = (SELECT Id FROM Partners WHERE FirstName = 'Jane' AND LastName = 'Smith');

INSERT INTO Policies (PolicyNumber, PolicyAmount, PartnerId)
VALUES
('POLICY12345', 6000.00, @JohnDoeId);

INSERT INTO Policies (PolicyNumber, PolicyAmount, PartnerId)
VALUES
('POLICY67890', 2000.00, @JaneSmithId),
('POLICY23456', 1500.00, @JaneSmithId),
('POLICY98765', 2500.00, @JaneSmithId);