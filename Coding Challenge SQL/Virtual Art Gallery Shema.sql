CREATE TABLE Artists (
ArtistID INT PRIMARY KEY,
Name VARCHAR(255) NOT NULL,
Biography TEXT,
Nationality VARCHAR(100));

CREATE TABLE Categories (
CategoryID INT PRIMARY KEY,
Name VARCHAR(100) NOT NULL);

CREATE TABLE Artworks (
ArtworkID INT PRIMARY KEY,
Title VARCHAR(255) NOT NULL,
ArtistID INT,
CategoryID INT,
Year INT,
Description TEXT,
ImageURL VARCHAR(255),
FOREIGN KEY (ArtistID) REFERENCES Artists (ArtistID),
FOREIGN KEY (CategoryID) REFERENCES Categories (CategoryID));

CREATE TABLE Exhibitions (
ExhibitionID INT PRIMARY KEY,
Title VARCHAR(255) NOT NULL,
StartDate DATE,
EndDate DATE,
Description TEXT);

CREATE TABLE ExhibitionArtworks (
ExhibitionID INT,
ArtworkID INT,
PRIMARY KEY (ExhibitionID, ArtworkID),
FOREIGN KEY (ExhibitionID) REFERENCES Exhibitions (ExhibitionID),
FOREIGN KEY (ArtworkID) REFERENCES Artworks (ArtworkID));

INSERT INTO Artists (ArtistID, Name, Biography, Nationality) VALUES
(1, 'Pablo Picasso', 'Renowned Spanish painter and sculptor.', 'Spanish'),
(2, 'Vincent van Gogh', 'Dutch post-impressionist painter.', 'Dutch'),
(3, 'Leonardo da Vinci', 'Italian polymath of the Renaissance.', 'Italian');

INSERT INTO Categories (CategoryID, Name) VALUES
(1, 'Painting'),
(2, 'Sculpture'),
(3, 'Photography');

INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES
(1, 'Starry Night', 2, 1, 1889, 'A famous painting by Vincent van Gogh.', 'starry_night.jpg'),
(2, 'Mona Lisa', 3, 1, 1503, 'The iconic portrait by Leonardo da Vinci.', 'mona_lisa.jpg'),
(3, 'Guernica', 1, 1, 1937, 'Pablo Picasso''s powerful anti-war mural.', 'guernica.jpg');

INSERT INTO Exhibitions (ExhibitionID, Title, StartDate, EndDate, Description) VALUES
(1, 'Modern Art Masterpieces', '2023-01-01', '2023-03-01', 'A collection of modern art masterpieces.'),
(2, 'Renaissance Art', '2023-04-01', '2023-06-01', 'A showcase of Renaissance art treasures.');

INSERT INTO ExhibitionArtworks (ExhibitionID, ArtworkID) VALUES
(1, 1),
(1, 2),
(1, 3),
(2, 2);

select * from Artists

select * from Artworks

select * from ExhibitionArtworks

select * from Exhibitions

select * from Categories

--1. Retrieve the names of all artists along with the number of artworks they have in the gallery, and
--list them in descending order of the number of artworks.
select a.name, count(ar.artworkid) as artworkcount
from artists a
join artworks ar on a.artistid = ar.artistid
group by a.name
order by artworkcount desc

--2. List the titles of artworks created by artists from 'Spanish' and 'Dutch' nationalities, and order
--them by the year in ascending order.
select ar.title, ar.year
from artworks ar
join artists a on ar.artistid = a.artistid
where a.nationality in ('spanish', 'dutch')
order by ar.year asc

--3. Find the names of all artists who have artworks in the 'Painting' category, and the number of
--artworks they have in this category.
select a.name, count(ar.artworkid) as Paintingcount 
from artists a
join artworks ar on a.artistid = ar.artistid
join categories c on ar.categoryid = c.categoryid
where c.name = 'painting'
group by a.name

--4.List the names of artworks from the 'Modern Art Masterpieces' exhibition, along with their
--artists and categories.
select ar.title, a.name as Artistname, c.name as Categoryname
from artworks ar
join exhibitionartworks ea on ar.artworkid = ea.artworkid
join exhibitions e on ea.exhibitionid = e.exhibitionid
join artists a on ar.artistid = a.artistid
join categories c on ar.categoryid = c.categoryid
where e.title = 'Modern Art Masterpieces'

--5. Find the artists who have more than two artworks in the gallery.
select a.name from artists a
join artworks ar on a.artistid = ar.artistid
group by a.name
having count(ar.artworkid) > 2

--6. Find the titles of artworks that were exhibited in both 'Modern Art Masterpieces' and 'Renaissance Art' exhibitions.
select ar.title from artworks ar
join exhibitionartworks ea1 on ar.artworkid = ea1.artworkid
join exhibitions e1 on ea1.exhibitionid = e1.exhibitionid
join exhibitionartworks ea2 on ar.artworkid = ea2.artworkid
join exhibitions e2 on ea2.exhibitionid = e2.exhibitionid
where e1.title = 'Modern Art Masterpieces' and e2.title = 'Renaissance Art'

--7. Find the total number of artworks in each category.
select c.name as categoryname, count(ar.artworkid) as TotalArtworks
from categories c
left join artworks ar on c.categoryid = ar.categoryid
group by c.name

--8. List artists who have more than 3 artworks in the gallery.
select a.name from artists a
join artworks ar on a.artistid = ar.artistid
group by a.name
having count(ar.artworkid) > 3

--9. Find the artworks created by artists from a specific nationality (e.g., Spanish).
select ar.title from artworks ar
join artists a on ar.artistid = a.artistid
where a.nationality = 'Spanish'

--10. List exhibitions that feature artwork by both Vincent van Gogh and Leonardo da Vinci.
select e.title from exhibitions e
join exhibitionartworks ea1 on e.exhibitionid = ea1.exhibitionid
join artworks ar1 on ea1.artworkid = ar1.artworkid
join artists a1 on ar1.artistid = a1.artistid
join exhibitionartworks ea2 on e.exhibitionid = ea2.exhibitionid
join artworks ar2 on ea2.artworkid = ar2.artworkid
join artists a2 on ar2.artistid = a2.artistid
where a1.name = 'Vincent van Gogh' and a2.name = 'Leonardo da Vinci'

--11. Find all the artworks that have not been included in any exhibition.
select ar.title from artworks ar
join exhibitionartworks ea on ar.artworkid = ea.artworkid
where ea.artworkid is null

--12. List artists who have created artworks in all available categories.
select a.name from artists a
join artworks ar on a.artistid = ar.artistid
group by a.name
having count(distinct ar.categoryid) = (select count(*) from categories)

--13. List the total number of artworks in each category.
select c.name as categoryname, count(ar.artworkid) as TotalArtworks
from categories c
left join artworks ar on c.categoryid = ar.categoryid
group by c.name

--14. Find the artists who have more than 2 artworks in the gallery.
select a.name from artists a
join artworks ar on a.artistid = ar.artistid
group by a.name
having count(ar.artworkid) > 2

--15. List the categories with the average year of artworks they contain, only for categories with more than 1 artwork.
select c.name as CategoryName, avg(ar.year) as AvgYear
from categories c
join artworks ar on c.categoryid = ar.categoryid
group by c.name
having count(ar.artworkid) > 1

--16. Find the artworks that were exhibited in the 'Modern Art Masterpieces' exhibition.
select ar.title, a.name as Artistname
from artworks ar
join exhibitionartworks ea on ar.artworkid = ea.artworkid
join exhibitions e on ea.exhibitionid = e.exhibitionid
join artists a on ar.artistid = a.artistid
join categories c on ar.categoryid = c.categoryid
where e.title = 'Modern Art Masterpieces'

--17. Find the categories where the average year of artworks is greater than the average year of all artworks.
select c.name as CategoryName
from categories c
join artworks ar on c.categoryid = ar.categoryid
group by c.name
having avg(ar.year) > (select avg(year) from artworks)

--18. List the artworks that were not exhibited in any exhibition.
select ar.title from artworks ar
join exhibitionartworks ea on ar.artworkid = ea.artworkid
where ea.artworkid is null

--19. Show artists who have artworks in the same category as "Mona Lisa."
select distinct a.name from artists a
join artworks ar1 on a.artistid = ar1.artistid
where ar1.categoryid = 
(select categoryid from artworks
where title = 'Mona Lisa')

--20. List the names of artists and the number of artworks they have in the gallery.
select a.name, count(ar.artworkid) as artworkcount
from artists a
left join artworks ar on a.artistid = ar.artistid
group by a.name

