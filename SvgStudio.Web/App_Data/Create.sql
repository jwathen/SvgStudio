create table Licenses
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	AttributionRequired bit not null,
	LicenseName nvarchar(1024) null,
	LicenseUrl nvarchar(1024) null
)
go

create table MarkupFragments
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	Content nvarchar(max) not null
)
go

create table Templates
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null default(1),
	IsMaster bit not null default(1),
	Name nvarchar(max) not null
)
go

create table CompatibilityTags
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	Tag nvarchar(128) not null
)
go

create index IX_CompatibilityTags_Tag on CompatibilityTags(Tag);

create table Shapes
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null default(1),
	ShapeType int not null,
	Name nvarchar(max) not null,
	Width int not null,
	Height int not null,
	NumberOfFillsSupported int not null,
	NumberOfStrokesSupported int not null,
	SortOrder smallint not null default(32767),
	BasicShape_MarkupFragmentId varchar(32) null,
	TemplateShape_TemplateId varchar(32) null,
	TemplateShape_ClipPathMarkupFragmentId  varchar(32) null
)
go

create index IX_Shapes_ShapeType on Shapes(ShapeType);
create index IX_Shapes_BasicShape_MarkupFragmentId on Shapes(BasicShape_MarkupFragmentId);
create index IX_Shapes_TemplateShape_TemplateId on Shapes(TemplateShape_TemplateId);
create index IX_Shapes_TemplateShape_ClipPathMarkupFragmentId on Shapes(TemplateShape_ClipPathMarkupFragmentId);

create table Shape_CompatibilityTag
(
	Id varchar(65) not null primary key,
	CompatibilityTagId varchar(32) not null,
	ShapeId varchar(32) not null
)
go

create table ContentLicenses
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	LicenseId varchar(32) not null,
	ShapeId varchar(32) null,
	ContentUrl nvarchar(1024) null,
	AttributionName nvarchar(1024) null,
	AttributionUrl nvarchar(1024) null
)
go

create index IX_ConentLicenses_LicenseId on ContentLicenses(LicenseId);
create index IX_ConentLicenses_ShapeId on ContentLicenses(ShapeId);

create table DesignRegions
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null default(1),
	Name nvarchar(max) not null,
	X int not null,
	Y int not null,
	Width int not null,
	Height int not null,
	SortOrder smallint not null default(32767),
	TemplateId varchar(32) not null
)
go

create index IX_DesignRegions_TemplateId on DesignRegions(TemplateId);

create table DesignRegion_CompatibilityTag
(
	Id varchar(65) not null primary key,
	CompatibilityTagId varchar(32) not null,
	DesignRegionId varchar(32) not null
)
go

create table Palettes
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null default(1),
	Name nvarchar(max) null,
	SortOrder smallint not null default(32767),
)
go

create table Designs
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	ShapeId varchar(32) not null,
	PaletteId varchar(32) not null
)
go

create index IX_Designs_ShapeId on Designs(ShapeId);
create index IX_Designs_PaletteId on Designs(PaletteId);

create table Strokes
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null default(1),
	Color nvarchar(50) not null,
	Width int not null,
	DashArray nvarchar(max) null,
	PaletteId varchar(32) null
)
go

create index IX_Strokes_PaletteId on Strokes(PaletteId);

create table Fills
(
	Id varchar(32) not null primary key,
	RowVersion rowversion not null,
	InsertDateUtc datetime not null default(getutcdate()),
	IsActive bit not null default(1),
	FillType int not null,
	PaletteId varchar(32) null,
	SolidColorFill_Color nvarchar(50) null,
	PatternFill_Name nvarchar(50) null,
	PatternFill_X int null,
	PatternFill_Y int null,
	PatternFill_Width float null,
	PatternFill_Height float null,
	PatternFill_PatternUnits nvarchar(50) null,
	PatternFill_PatternContentUnits nvarchar(50) null,
	PatternFill_DesignId varchar(32) null
)
go

create index IX_Fills_PaletteId on Fills(PaletteId);
create index IX_Fills_FillType on Fills(FillType);
create index IX_Fills_PatternFill_DesignId on Fills(PatternFill_DesignId);