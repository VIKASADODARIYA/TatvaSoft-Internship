create table Missions (
    Id serial primary key,
	MissionTitle character varying not null,
	MissionDescription text,
	MissionOrganizationName character varying,
	MissionOrganizationDetails text,
	CountryId integer references Country(Id),
	CityId integer references City(Id),
	StartDate date not null,
	EndDate date not null,
	MissionType character varying(200),
	TotalSheets integer,
	RegistrationDeadLine date,
	MissionThemeId integer references MissionTheme(Id),
	MissionSkillId integer references MissionSkill(Id),
	MissionImage text,
	MissionDocuments text,
	MissionAvailability text,
	MissionVideoUrl text
);

select * from Missions

INSERT INTO Missions (
    MissionTitle,
    MissionDescription,
    MissionOrganizationName,
    MissionOrganizationDetails,
    CountryId,
    CityId,
    StartDate,
    EndDate,
    MissionType,
    TotalSheets,
    RegistrationDeadline,
    MissionThemeId,
    MissionSkillId,
    MissionImage,
    MissionDocuments,
    MissionAvailability,
    MissionVideoUrl
) VALUES (
    'Sample Mission',
    'This is a sample mission description.',
    'Sample Organization',
    'Details about the sample organization.',
    1, -- Assuming CountryId 1 exists
    1, -- Assuming CityId 1 exists
    '2024-05-21', -- Start Date
    '2024-06-21', -- End Date
    'Exploratory',
    10,
    '2024-05-15', -- Registration Deadline
    1, -- Assuming MissionThemeId 1 exists
    1, -- Assuming MissionSkillId 1 exists
    'http://example.com/image.jpg',
    'http://example.com/documents.pdf',
    'Full-time',
    'http://example.com/video.mp4'
);

