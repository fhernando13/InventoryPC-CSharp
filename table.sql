use master;

create database pcinfo;
use pcinfo;

CREATE TABLE dbo.pc
    (IdMachine int IDENTITY PRIMARY KEY NOT NULL,
    MachineName varchar(5) NOT NULL,
    BrandMB varchar(25) NOT NULL,
    ModelMB varchar(25) NOT NULL,
    NoSerieMB varchar(50) NOT NULL,
    CorporationSO varchar(50) NOT NULL,
    NameSO varchar(50) NOT NULL,
    VersionSO varchar(50) NOT NULL,
    archSO varchar(7) NOT NULL,
    NumberSerialSO varchar(50) NOT NULL,
    KeyActivation varchar(30) NOT NULL,
    NameProcessor varchar(50) NOT NULL,
    ManufacturerProcessor varchar(50) NOT NULL,
    NumberOfCores varchar(50) NOT NULL,
    RoleProcessor varchar(50) NOT NULL,
    ProcessorId varchar(50) NOT NULL,
    ModelSSD varchar(50) NOT NULL,
    SizeSSD varchar(50) NOT NULL,
    NumberSerialSSD varchar(50) NOT NULL,
    SlotOneBrandRam varchar(50) NOT NULL,
    SlotOneNumberSerialRam varchar(50) NOT NULL,
    SlotOneStorageRam varchar(50) NOT NULL,
    SlotTwoBrandRam varchar(50) NOT NULL,
    SlotTwoNumberSerialRam varchar(50) NOT NULL,
    SlotTwoStorageRam varchar(50) NOT NULL,
    SlotTreeBrandRam varchar(50) NOT NULL,
    SlotTreeNumberSerialRam varchar(50) NOT NULL,
    SlotTreeStorageRam varchar(50) NOT NULL,
    SlotFourBrandRam varchar(50) NOT NULL,
    SlotFourNumberSerialRam varchar(50) NOT NULL,
    SlotFourStorageRam varchar(50) NOT NULL,
    CreateAT SmallDateTime,
    UpdateAt SmallDateTime  
    );

select * from pcinfo;


CREATE TABLE dbo.employee
(
    Idemployee int IDENTITY PRIMARY KEY NOT NULL,
    Name VARCHAR (50) NOT NULL,
    Lastname VARCHAR (50) NOT NULL,
    Age int NOT NULL,
    CreateAT SmallDateTime,
    UpdateAt SmallDateTime
);