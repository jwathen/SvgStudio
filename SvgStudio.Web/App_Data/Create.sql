create table Shapes
(
	Id int not null primary key identity(1,1),
	ShapeType nvarchar(50) not null,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null,
	Name nvarchar(max) not null,
	Width int not null,
	Height int not null,
	NumberOfFillsSupported int not null,
	NumberOfStrokesSupported int not null,
	Markup xml not null,
	SourceUrl nvarchar(max) null
)
go

create index IX_Shapes_ShapeType on Shapes(ShapeType);

create table Templates
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null,
	Name nvarchar(max) not null
)
go

create table DesignRegions
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null,
	Name nvarchar(max) not null,
	X int not null,
	Y int not null,
	Width int not null,
	Height int not null,
	TemplateId int not null references Templates(Id)
)
go

create index IX_DesignRegions_TemplateId on DesignRegions(TemplateId);

create table ShapeDesignRegionCompatibility
(
	DesignRegionId int not null references DesignRegions(Id),
	ShapeId int not null references Shapes(Id),
	constraint ShapeDesignRegionCompatibility_PK primary key (DesignRegionId, ShapeId)
)
go

create table Palettes
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null,
	Name nvarchar(max) null
)
go

create table Designs
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	ShapeId int not null references Shapes(Id),
	PaletteId int not null references Palettes(Id)
)
go

create index IX_Designs_ShapeId on Designs(ShapeId);
create index IX_Designs_PaletteId on Designs(PaletteId);

create table Strokes
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null,
	Color nvarchar(50) not null,
	Width int not null,
	DashArray nvarchar(max) null,
	PaletteId int null references Palettes(Id)
)
go

create index IX_Strokes_PaletteId on Strokes(PaletteId);

create table Fills
(
	Id int not null primary key identity(1,1),
	FillType nvarchar(50) not null,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null,
	PaletteId int null references Palettes(Id),
	SolidColorFill_Color nvarchar(50) null,
	PatternFill_Name nvarchar(50) null,
	PatternFill_X int null,
	PatternFill_Y int null,
	PatternFill_Width float null,
	PatternFill_Height float null,
	PatternFill_PatternUnits nvarchar(50) null,
	PatternFill_PatternContentUnits nvarchar(50) null,
	PatternFill_DesignId int null references Designs(Id)
)
go

create index IX_Fills_PaletteId on Fills(PaletteId);
create index IX_Fills_FillType on Fills(FillType);
create index IX_Fills_PatternFill_DesignId on Fills(PatternFill_DesignId);