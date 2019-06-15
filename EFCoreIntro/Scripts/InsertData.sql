INSERT INTO Employees (FirstName, LastName, DateHired, SupervisorId) VALUES (N'John', N'Doe', '2015-01-10', NULL);
INSERT INTO Employees (FirstName, LastName, DateHired, SupervisorId) VALUES (N'Jane', N'Doe', '2015-02-20', 1);
INSERT INTO Employees (FirstName, LastName, DateHired, SupervisorId) VALUES (N'Tom', N'Smith', '2016-06-19', 2);
INSERT INTO Employees (FirstName, LastName, DateHired, SupervisorId) VALUES (N'Bob', N'Lee', '2016-06-20', 2);

GO

INSERT INTO Projects (Name, LeaderId) VALUES (N'Firestone', 1);
INSERT INTO Projects (Name, LeaderId) VALUES (N'Blue', 2);

GO
