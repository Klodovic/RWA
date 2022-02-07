/*Tablica za registraciju novih korisnika*/
/*Tablica se može dodati u AdventureWorksOBP bazu bez povezivanja s ostalim tablicama*/
create table RegisteredUser
(
	IDUser int primary key identity,
	UserName nvarchar(50) not null,
	UserEmail nvarchar(50) not null unique,
	UserPassword nvarchar(600) not null,
)
go
/*PROCEDURE*/

/*Procedura za kreiranje novog korisnika*/
create proc CreateNewUser
	@UserName nvarchar(50),
	@Email nvarchar(50),
	@Password nvarchar(600)
as
begin
	declare @count int
	declare @returnCode int

	select @count=COUNT(UserEmail) from RegisteredUser
	where UserEmail = @Email

	if @count > 0
		begin
			set @returnCode = -1
		end
	else
		begin
			set	@returnCode = 1
			insert into RegisteredUser values (@UserName, @Email, @Password)
		end
	select @returnCode as returnValue

end
go

/*Procedura za login user-a*/
create proc UserLogin
	@userName nvarchar(50),
	@password nvarchar(512)
as
begin
	declare @count int
	declare @returnCode int

	select @count = COUNT(UserName) from RegisteredUser
	where UserName = @userName and [UserPassword] = @password

	if @count = 1
		begin
			set @returnCode = 1
		end
	else
		begin
			set @returnCode = -1
		end
	select @returnCode as returnValue
end
go

/*Dohvat svih drzava*/
create proc GetAllCountries
as
begin
	select * from Drzava
end
go

/*Dohvat svih gradova po IdDrzava*/
create proc GetAllCities
	@drzavaId int
as
begin
	select * from Grad where DrzavaID = @drzavaId
end
go

/*Dohvat Kupaca za GridView*/
create proc GetTopBuyersByCityByCountry
	@numOfBuyers int,
	@drzavaId int,
	@gradId int
as
begin
	select top (@numOfBuyers) * from Kupac k
	join Grad g on g.IDGrad=k.GradID
	join Drzava d on d.IDDrzava=g.DrzavaID
	where GradID = @gradId and DrzavaID = @drzavaId
end
go

/*Dohvat jednog kupca po IDKupac*/
create proc GetBuyerByID
	@IdKupac int
as
begin
	select * from Kupac
	where IDKupac=@IdKupac
end
go

/*Dohvat drzave kupca po IDKupac*/
create proc GetBuyersCountryByID
	@IdKupac int
as
begin 
	select d.IDDrzava from Drzava d
		where d.IDDrzava in (select g.DrzavaID from Grad g where g.IDGrad in 
			(select k.GradID from Kupac k where k.IDKupac=@IdKupac))
end
go

/*Ažuriranje odabranog kupca*/
create proc UpdateSingleBuyer
	@IdKupac int,
	@Ime nvarchar(50),
	@Prezime nvarchar(50),
	@Email nvarchar(50),
	@Telefon nvarchar(50),
	@GradID int
as
begin
	declare @returnCode int
	begin try
		begin tran
			UPDATE Kupac SET Ime=@Ime, Prezime=@Prezime, Email=@Email, Telefon=@Telefon,GradID=@GradID 
			where IDKupac=@IdKupac
		commit tran
		set	@returnCode = 1
	end try
	begin catch
		if @@TRANCOUNT > 0
		begin
			rollback tran
			set @returnCode = -1
		end
	end catch
end
go

/*Dohvati sve račune odabranog kupca*/
create proc GetAllReceiptOfBuyerByID
	@IdKupac int
as
begin
	select * from Racun
	where KupacID=@IdKupac
end
go

/*Dohvati sve komercijaliste za odabranog kupca*/
create proc GetSalesOfficerByID
	@IdKomercijalist int
as 
begin
	select * from Komercijalist
	where IDKomercijalist=@IdKomercijalist
end
go

/*Dohvati sve Kreditne kartice za odabranog kupca*/
create proc GetCreditCardByID
	@IdKreditnaKartica int
as 
begin
	select * from KreditnaKartica
	where IDKreditnaKartica=@IdKreditnaKartica
end
go

/*Dohvati odabranog kupca za račun*/
create proc GetReceitBuyerByID
	@IdKupac int
as
begin
	select * from Kupac
	where IDKupac=@IdKupac
end
go

/*Dohvati stavku za odabrani račun*/
create proc GetItemsByReceiptID
	@IdRacun int
as
begin
	select s.IDStavka, s.RacunID, s.Kolicina, s.CijenaPoKomadu, s.PopustUPostocima, s.UkupnaCijena,
	p.IDProizvod, p.Naziv, p.BrojProizvoda, p.Boja, p.MinimalnaKolicinaNaSkladistu, p.CijenaBezPDV,
	pk.IDPotkategorija, pk.Naziv as NazivPotkategorije,
	kt.IDKategorija, kt.Naziv as NazivKategorije,
	k.IDKomercijalist, k.Ime, k.Prezime,
	kk.IDKreditnaKartica, kk.Tip, kk.Broj
	from Stavka s
	join Proizvod p on p.IDProizvod=s.ProizvodID
	join Racun r on r.IDRacun=s.RacunID
	join Potkategorija pk on pk.IDPotkategorija=p.PotkategorijaID
	join Kategorija kt on kt.IDKategorija=pk.KategorijaID
	join Komercijalist k on k.IDKomercijalist=r.KomercijalistID
	join KreditnaKartica kk on kk.IDKreditnaKartica=r.KreditnaKarticaID
	where RacunID=@IdRacun
end
go

/*Dohvati proizvod za odabrani račun*/
create proc GetItemBundleDetailsByID
	@IdStavka int
as
begin
	select s.IDStavka, s.RacunID, s.Kolicina, s.CijenaPoKomadu, s.PopustUPostocima, s.UkupnaCijena,
	p.IDProizvod, p.Naziv, p.BrojProizvoda, p.Boja, p.MinimalnaKolicinaNaSkladistu, p.CijenaBezPDV,
	pk.IDPotkategorija, pk.Naziv as NazivPotkategorije,
	kt.IDKategorija, kt.Naziv as NazivKategorije,
	k.IDKomercijalist, k.Ime, k.Prezime,
	kk.IDKreditnaKartica, kk.Tip, kk.Broj
	from Stavka s
	join Proizvod p on p.IDProizvod=s.ProizvodID
	join Racun r on r.IDRacun=s.RacunID
	join Potkategorija pk on pk.IDPotkategorija=p.PotkategorijaID
	join Kategorija kt on kt.IDKategorija=pk.KategorijaID
	join Komercijalist k on k.IDKomercijalist=r.KomercijalistID
	join KreditnaKartica kk on kk.IDKreditnaKartica=r.KreditnaKarticaID
	where IDStavka=@IdStavka
end
go

/*Dohvati proizvod za odabrani račun*/
create proc GetAllSubCategories
as
begin
	select * from Potkategorija 
end
go

/*Dohvati sve kategorije*/
create proc GetCategories
as
begin
	select * from Kategorija 
end
go

/*Dohvati sve potkategorije po IDKategorija*/
create proc GetSubCategoriesByID
	@IdKategorija int
as
begin
	select * from Potkategorija where KategorijaID=@IdKategorija
end
go

/*Ažuriraj stavku*/
create proc GetUpdateBundleItemsByIDs
	@IDStavka int, @Kolicina smallint, @CijenaPoKomadu money, @PopustUPostocima money, @UkupnaCijena money,
	@IdProizvod int, @NazivProizvoda nvarchar(50), @BrojProizvoda nvarchar(25), @Boja nvarchar(15), 
	@MinimalnaKolicinaNaSkladistu smallint, @CijenaBezPDV money,
	@IdPotkategorija int
as
begin
	declare @returnCode int
	begin try
		begin tran
			UPDATE Stavka set Kolicina=@Kolicina, CijenaPoKomadu=@CijenaPoKomadu, PopustUPostocima=@PopustUPostocima, UkupnaCijena=@UkupnaCijena
			where IDStavka=@IDStavka

			UPDATE Proizvod set Naziv = @NazivProizvoda, BrojProizvoda = @BrojProizvoda, Boja = @Boja, MinimalnaKolicinaNaSkladistu = @MinimalnaKolicinaNaSkladistu, CijenaBezPDV = @CijenaBezPDV
			where IDProizvod = @IdProizvod
		
		commit tran
		set	@returnCode = 1
	end try
	begin catch
		if @@TRANCOUNT > 0
		begin
			rollback tran
			set @returnCode = -1
		end
	end catch
end
go

------------------------------------------------------------------
/*Dohvati sve proizvode*/
create proc GetAllProducts
as
begin
	select * from Proizvod
end
go

/*Dohvati sve potkategorije po id*/
create proc GetAllSubCategoriesByID
	@IdPotkategorija int
as
begin
	select * from Potkategorija	where IDPotkategorija = @IdPotkategorija
end
go

/*Dohvati detalje proizvoda po ID*/
create proc GetProductsDetails
	@IdProizvod int
as
begin
	select * from Proizvod	where IDProizvod = @IdProizvod
end
go

/*Ažuriranje odabranog proizvoda*/
create proc ProductUpdate
	@IdProizvod int, @Naziv nvarchar(50), @BrojProizvoda nvarchar(25), @Boja nvarchar(15), 
	@MinimalnaKolicinaNaSkladistu smallint, @CijenaBezPDV money, @PotkategorijaID int
as
begin
	declare @returnCode int
	begin try
		begin tran
			UPDATE Proizvod SET Naziv = @Naziv, BrojProizvoda = @BrojProizvoda, Boja = @Boja, 
			MinimalnaKolicinaNaSkladistu = @MinimalnaKolicinaNaSkladistu, CijenaBezPDV = @CijenaBezPDV, 
			PotkategorijaID = @PotkategorijaID where IDProizvod = @IdProizvod
		commit tran
		set	@returnCode = 1
	end try
	begin catch
		if @@TRANCOUNT > 0
		begin
			rollback tran
			set @returnCode = -1
		end
	end catch
end
go

/*Dodavanje novog proizvoda*/
create proc InsertNewProduct
	@Naziv nvarchar(50), @BrojProizvoda nvarchar(25), @Boja nvarchar(15), 
	@MinimalnaKolicinaNaSkladistu smallint, @CijenaBezPDV money, @PotkategorijaID int
as
begin
	declare @returnCode int
	begin try
		begin tran
			INSERT into Proizvod (Naziv, BrojProizvoda, Boja, MinimalnaKolicinaNaSkladistu, CijenaBezPDV, PotkategorijaID) 
			values (@Naziv, @BrojProizvoda, @Boja, @MinimalnaKolicinaNaSkladistu, @CijenaBezPDV, @PotkategorijaID) 
		commit tran
		set	@returnCode = 1
	end try
	begin catch
		if @@TRANCOUNT > 0
		begin
			rollback tran
			set @returnCode = -1
		end
	end catch
end
go

-------------------------------------RESTfull-----------------------------------------------------------
/* Dohvati sve artikle (proizvode)*/
create proc GetAllApiProducts
as
	begin
		select * from Proizvod
	end
go

/* Dohvati određeni artikl (proizvod) po ID*/
create proc GetApiProductByID
	@IDProizvod int
as
	begin
		select * from Proizvod where IDProizvod = @IDProizvod
	end
go

/* Obriši određeni artikl (proizvod) po ID*/
create proc DeleteProductByID
	@IDProizvod int
as
	begin
		delete from Proizvod where IDProizvod = @IDProizvod
	end
go
-----------------------------------------------------------------------
/*************************************************************************/
---------------------------------------------------------------------------




