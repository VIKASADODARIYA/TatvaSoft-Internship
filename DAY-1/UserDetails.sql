create table UserDetails(
	Id serial primary key,
	UserId integer references "User"(Id),
	"Name" character varying,
	Surname character varying,
	EmployeeId character varying,
	Manager character varying,
	Title character varying,
	Department character varying,
	MyProfile text,
	WhyIVolunteer text,
	CountryId integer references Country(Id),
	CityId integer references City(Id),
	Availability text,
	LinkedInUrl text,
	MySkills text,
	UserImage text,
	Status boolean
);

select * from UserDetails

INSERT INTO UserDetails (
    UserId,
    "Name",
    Surname,
    EmployeeId,
    Manager,
    Title,
    Department,
    MyProfile,
    WhyIVolunteer,
    CountryId,
    CityId,
    Availability,
    LinkedInUrl,
    MySkills,
    UserImage,
    Status
) VALUES (
    1, -- Assuming UserId 1 exists
    'Vikas',
    'Adodariya',
    'EMP001',
    'Manager Name',
    'Software Engineer',
    'Engineering',
    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
    1, -- Assuming CountryId 1 exists
    1, -- Assuming CityId 1 exists
    'Full-time',
    'https://www.linkedin.com/in/johndoe',
    'Java, Python, SQL',
    'https://example.com/user.jpg',
    true -- Status (true for active, false for inactive)
);
