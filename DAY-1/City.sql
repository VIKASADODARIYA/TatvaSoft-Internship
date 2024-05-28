create table City(
	Id serial primary key,
	CountryId integer not null REFERENCES Country(Id),
	CityName character varying(100) not null
);

select * from City

INSERT INTO City (CountryId, CityName)
VALUES 
    (1, 'Junagadh'),
    (1, 'Mumbai'),
    (1, 'Delhi'),
    (2, 'Melbourne'),
	(2, 'Sydney'),
	(3, 'San Francisco'),
	(3, 'Las Vegas'),
	(3, 'New York'),
	(4, 'Tokyo'),

delete from City
ALTER SEQUENCE city_id_seq RESTART WITH 1;
