create table Licenses
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	AttributionRequired bit not null,
	LicenseName nvarchar(1024) null,
	LicenseUrl nvarchar(1024) null
)
go

create table MarkupFragments
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	Content nvarchar(max) not null
)
go

create table Templates
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null,
	IsMaster bit not null default(1),
	Name nvarchar(max) not null
)
go

create table CompatibilityTags
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	Tag nvarchar(128) not null
)
go

create index IX_CompatibilityTags_Tag on CompatibilityTags(Tag);

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
	SortOrder smallint not null default(32767),
	BasicShape_MarkupFragmentId int null references MarkupFragments(Id),
	TemplateShape_TemplateId int null references Templates(Id),
	TemplateShape_ClipPathMarkupFragmentId  int null references MarkupFragments(Id)
)
go

create index IX_Shapes_ShapeType on Shapes(ShapeType);
create index IX_Shapes_BasicShape_MarkupFragmentId on Shapes(BasicShape_MarkupFragmentId);
create index IX_Shapes_TemplateShape_TemplateId on Shapes(TemplateShape_TemplateId);
create index IX_Shapes_TemplateShape_ClipPathMarkupFragmentId on Shapes(TemplateShape_ClipPathMarkupFragmentId);

create table Shape_CompatibilityTag
(
	CompatibilityTagId int not null references CompatibilityTags(Id),
	ShapeId int not null references Shapes(Id),
	constraint Shape_CompatibilityTag_PK primary key (CompatibilityTagId, ShapeId)
)
go

create table ContentLicenses
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	LicenseId int not null  references Licenses(Id),
	ShapeId int null  references Shapes(Id),
	ContentUrl nvarchar(1024) null,
	AttributionName nvarchar(1024) null,
	AttributionUrl nvarchar(1024) null
)
go

create index IX_ConentLicenses_LicenseId on ContentLicenses(LicenseId);
create index IX_ConentLicenses_ShapeId on ContentLicenses(ShapeId);

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
	SortOrder smallint not null default(32767),
	TemplateId int not null references Templates(Id)
)
go

create index IX_DesignRegions_TemplateId on DesignRegions(TemplateId);

create table DesignRegion_CompatibilityTag
(
	CompatibilityTagId int not null references CompatibilityTags(Id),
	DesignRegionId int not null references DesignRegions(Id),
	constraint DesignRegion_CompatibilityTag_PK primary key (CompatibilityTagId, DesignRegionId)
)
go

create table Palettes
(
	Id int not null primary key identity(1,1),
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null,
	Name nvarchar(max) null,
	SortOrder smallint not null default(32767),
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