Create table Customer(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	ContactNumber VARCHAR(20) NOT NULL,
	EmailAddress VARCHAR(100) NOT NULL,
	Address TEXT NOT NULL,
	TaxExempted BIT NOT NULL,
	CONSTRAINT UK_Customer_Email UNIQUE(EmailAddress)
);

Create table Booking (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	CustomerId INT NOT NULL,
	BillingMode INT NOT NULL,
	CreatedAt DATETIME NOT NULL
);

ALTER TABLE Booking
ADD CONSTRAINT FK_Booking_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id) ON UPDATE CASCADE ON DELETE CASCADE;

Create table Delivery(
	BookingId INT PRIMARY KEY,
	ConsigneeName VARCHAR(100) NOT NULL,
	ConsigneeContactNumber VARCHAR(20) NOT NULL,
	ConsigneeAddress TEXT NOT NULL,
	Status INT NOT NULL
);

ALTER TABLE Delivery
ADD CONSTRAINT FK_Delivery_Booking FOREIGN KEY(BookingId) REFERENCES Booking(Id) ON UPDATE CASCADE ON DELETE CASCADE;

Create table Invoice(
	BookingId INT PRIMARY KEY,
	Total DECIMAL(18,2) NOT NULL
);

ALTER TABLE Invoice
ADD CONSTRAINT FK_Invoice_Booking FOREIGN KEY(BookingId) REFERENCES Booking(Id) ON UPDATE CASCADE ON DELETE CASCADE;

Create table Location(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	CONSTRAINT UK_Location UNIQUE(Name)
);

Create table Package(
	BookingId INT PRIMARY KEY,
	Description TEXT NOT NULL,
	TrackingNumber VARCHAR(255) DEFAULT NULL
);

ALTER TABLE Package
ADD CONSTRAINT FK_Package_Booking FOREIGN KEY(BookingId) REFERENCES Booking(Id) ON UPDATE CASCADE ON DELETE CASCADE;

Create table PackageLog(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	PackageId INT NOT NULL,
	UserId NVARCHAR(450) NOT NULL,
	Mode INT NOT NULL,
	LoggedAt DATETIME NOT NULL
);

ALTER TABLE PackageLog
ADD CONSTRAINT FK_PackageLog_Package FOREIGN KEY(PackageId) REFERENCES Package(BookingId) ON UPDATE CASCADE ON DELETE CASCADE;

Create table PaymentMethod(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	CONSTRAINT UK_PaymentMethod UNIQUE(Name)
);

Create table Payment(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	InvoiceId INT NOT NULL,
	PaymentMethodId INT NOT NULL,
	Amount DECIMAL(18, 9) NOT NULL,
	UserId NVARCHAR(450) NOT NULL,
	ProcessedAt DATETIME NOT NULL
);

ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_Invoice FOREIGN KEY(InvoiceId) REFERENCES Invoice(BookingId) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_PaymentMethod FOREIGN KEY(PaymentMethodId) REFERENCES PaymentMethod(Id) ON UPDATE CASCADE ON DELETE CASCADE;


Create table PaymentMethodField(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	PaymentMethodId INT NOT NULL,
	Name VARCHAR(100) NOT NULL,
	Type INT NOT NULL,
	CONSTRAINT UK_PaymentMethodField UNIQUE(Name)
);

ALTER TABLE PaymentMethodField
ADD CONSTRAINT FK_PaymentMethodField_PaymentMethod FOREIGN KEY(PaymentMethodId) REFERENCES PaymentMethod(Id) ON UPDATE CASCADE ON DELETE CASCADE;

Create table PaymentMethodFieldValue(
	PaymentId INT NOT NULL,
	PaymentMethodFieldId INT NOT NULL,
	Value TEXT NOT NULL,
	PRIMARY KEY (PaymentId, PaymentMethodFieldId)
);

ALTER TABLE PaymentMethodFieldValue
ADD CONSTRAINT FK_PaymentMethodFieldValue_PaymentMethodField FOREIGN KEY(PaymentMethodFieldId) REFERENCES PaymentMethodField(Id) ON UPDATE CASCADE ON DELETE CASCADE;
ALTER TABLE PaymentMethodFieldValue
ADD CONSTRAINT FK_PaymentMethodFieldValue_Payment FOREIGN KEY(PaymentId) REFERENCES Payment(Id);


Create table Permission(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	[Group] VARCHAR(100) NOT NULL,
	Name VARCHAR(100) NOT NULL,
	Description TEXT NOT NULL,
	CONSTRAINT UK_Permission UNIQUE(Name)
);

Create table Pickup(
	BookingId INT PRIMARY KEY,
	ContactNumber VARCHAR(20) NOT NULL,
	Address TEXT NOT NULL,
	Status INT NOT NULL
);

ALTER TABLE Pickup
ADD CONSTRAINT FK_Pickup_Booking FOREIGN KEY(BookingId) REFERENCES Booking(Id) ON UPDATE CASCADE ON DELETE CASCADE;

Create table Service(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	Cost DECIMAL(10,8) NOT NULL,
	CONSTRAINT UK_Service UNIQUE(Name)
);

Create table UserPermission(
	UserId NVARCHAR(450) NOT NULL,
	PermissionId INT NOT NULL,
	PRIMARY KEY(UserId, PermissionId)
);

ALTER TABLE UserPermission
ADD CONSTRAINT FK_UserPermission_User Foreign Key(UserId) references AspNetUsers(Id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE UserPermission
ADD CONSTRAINT FK_UserPermission_Permission Foreign Key(PermissionId) references Permission(Id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE PackageLog
ADD CONSTRAINT FK_PackageLog_User FOREIGN KEY(UserId) REFERENCES AspNetUsers(Id) ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_User FOREIGN KEY(UserId) REFERENCES AspNetUsers(Id);