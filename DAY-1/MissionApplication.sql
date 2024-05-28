create table MissionApplication (
    Id serial primary key,
    MissionId int references Missions(Id),
    UserId int references "User"(Id),
    AppliedDate timestamp not null,
    Status boolean,
    Sheet integer
);

select * from MissionApplication

INSERT INTO MissionApplication (
    MissionId,
    UserId,
    AppliedDate,
    Status,
    Sheet
) VALUES (
    1, -- Assuming MissionId 1 exists
    1, -- Assuming UserId 1 exists
    CURRENT_TIMESTAMP, -- Applied Date (current timestamp)
    true, -- Status (true for applied, false for not applied)
    5 -- Number of Sheets
);