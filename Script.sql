CREATE TABLE "Costumer" (
 	"Id" uuid not null primary key,
	"FirstName" varchar(50) not null,
	"LastName" varchar(50) not null,
	"Gender" char(1),
	"Email" varchar(100) unique,
	"PhoneNumber" int unique
);

create table "Reservation" (
	"Id" uuid not null primary key,
	"CostumerId" uuid,
	"ScreeningId" uuid,
	"PaidState" bool,
	"ActiveState" bool,
	constraint FK_Reservation_Screening_Id foreign key("ScreeningId") references "Screening"("Id"),
	constraint FK_Reservation_Costumer_Id FOREIGN KEY("CostumerId") references "Costumer"("Id")
);

create table "Cinema" (
	"Id" uuid not null primary key,
	"City" varchar(50),
	"CinemaName" varchar(50)
);

create table "Movie" (
	"Id" uuid not null primary key,
	"Name" varchar(50),
	"Langauge" varchar(50),
	"PgRating" varchar(10),
	"Price" numeric(15,4)
);

create table "Screening" (
	"Id" uuid not null primary key,
	"ScreenTime" timestamp,
	"CinemaMovieId" uuid not null,
	constraint FK_Screening_CinemaMovie_Id foreign key ("CinemaMovieId") references "CinemaMovie"("Id")
);

create table "CinemaMovie"(
	"Id" uuid not null primary key,
	"MovieId" uuid not null,
	"CinemaId" uuid not null,
	constraint FK_CinemaMovie_Movie_Id foreign key("MovieId") references "Movie"("Id"),
	constraint FK_CinemaMovie_Cinema_Id foreign key("CinemaId") references "Cinema"("Id")
);