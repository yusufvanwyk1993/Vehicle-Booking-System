# Vehicle-Booking-System
Basic vehicle booking system

Used:
1. visual studio 2019
2. dapper micro orm
3. progresql 12
4. dot net core 3.1
5. asp .net mvc

How to use vehicle booking system:
1. Go to clients tab and create clients.
2. Go to vehicles tab and create vehicles.
3. Go to bookings and create a new booking.


Database script:
```
Create new database
CREATE DATABASE "VehicleBookingDB"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_United States.1252'
    LC_CTYPE = 'English_United States.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;


--Create all required tables
CREATE TABLE public.client
(
    id serial PRIMARY KEY,
    firstname character varying(25)
);

CREATE TABLE public.vehicle
(
    id serial PRIMARY KEY,
    vehiclemodel character varying(25)
);

CREATE TABLE public.bookings
(
    id serial PRIMARY KEY,
    clientid integer NOT NULL,
    vehicleid integer NOT NULL,
    bookedfor date NOT NULL,
    notes character varying(25),
    FOREIGN KEY (clientid) REFERENCES public.client (id),
    FOREIGN KEY (vehicleid) REFERENCES public.vehicle (id)
);

--Create all required store procedures
CREATE OR REPLACE PROCEDURE public.bookingdelete(
	integer)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	DELETE FROM Bookings
	WHERE id=$1;
    COMMIT;
END;
$BODY$;

CREATE OR REPLACE PROCEDURE public.bookinginsert(
	_clientid integer,
	_vehicleid integer,
	_bookingdate character varying,
	_notes character varying)
LANGUAGE 'sql'
AS $BODY$
	INSERT INTO Bookings
	(
		clientid,
		vehicleid,
		bookedfor,
		notes
	)
	VALUES
	(
		_clientid,
		_vehicleid,
		to_timestamp(_bookingdate, 'DD/MM/YYYY'),
		_notes
	);
$BODY$;


CREATE OR REPLACE PROCEDURE public.clientdelete(
	integer)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	DELETE FROM Client
	WHERE id=$1;
    COMMIT;
END;
$BODY$;


CREATE OR REPLACE PROCEDURE public.clientinsert(
	_firstname character varying)
LANGUAGE 'sql'
AS $BODY$
	INSERT INTO Client
	(
		firstname
	)
	VALUES
	(
		_firstname
	);
$BODY$;


CREATE OR REPLACE PROCEDURE public.clientupdate(
	_clientid integer,
	_firstname character varying)
LANGUAGE 'sql'
AS $BODY$
		UPDATE Client 
		SET firstname = _firstname
		WHERE id = _clientid;
$BODY$;


CREATE OR REPLACE PROCEDURE public.vehicledelete(
	integer)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	DELETE FROM Vehicle
	WHERE id=$1;
    COMMIT;
END;
$BODY$;


CREATE OR REPLACE PROCEDURE public.vehicleinsert(
	_vehiclemodel character varying)
LANGUAGE 'sql'
AS $BODY$
	INSERT INTO Vehicle
	(
		vehiclemodel
	)
	VALUES
	(
		_vehiclemodel
	);
$BODY$;


CREATE OR REPLACE PROCEDURE public.vehicleupdate(
	_vehicleid integer,
	_vehiclemodel character varying)
LANGUAGE 'sql'
AS $BODY$
		UPDATE Vehicle 
		SET vehiclemodel = _vehiclemodel
		WHERE id = _vehicleid;
$BODY$;
```
