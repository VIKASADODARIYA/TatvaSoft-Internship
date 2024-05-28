create table Country (
   Id serial PRIMARY KEY,
   CountryName character varying(100) NOT NULL
);

select * from Country

insert into Country (CountryName) values ('India'),('Australia'),('America'),('Japan');

update Country set CountryName='Sri lanka' where Id='12';

delete from Country where id='12'

delete from Country

select * from Country where id='11'

select * from Country order by CountryName asc;

ALTER SEQUENCE country_id_seq RESTART WITH 1;
