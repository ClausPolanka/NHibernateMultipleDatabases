Mappings f�r mehrere Tabellen k�nnen im gleichen Projekt implementiert werden.
Sofern eine Tabelle f�r eine SessionFactory nicht vorhanden ist, wird das
Mapping ignoriert. Enth�lt eine Datenbank keine Tabellen werden alle Mappings
ignoriert. Wird jedoch auf eine Entit�t zugegriffen, die nicht in der Daten-
bank vorkomment, wird von NHibernate eine Exception geworfen.

Mit Hilfe der ExposeConfiguration Methode kann das DB-Schema automatisch erzeugt
werden. Dabei werden beim Erstellen das Schemas die Tabellen, sofern sie vorhanden
sind, gel�scht.