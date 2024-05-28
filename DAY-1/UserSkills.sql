create table UserSkills(
	Id serial primary key,
	Skills character varying not null,
	UserId integer references "User"(Id)
);

select * from UserSkills

INSERT INTO UserSkills (
    Skills,
    UserId
) VALUES (
    'Programming, Problem Solving, Communication',
    1 -- Assuming UserId 1 exists
);
