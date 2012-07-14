Mappings für mehrere Tabellen können im gleichen Projekt implementiert werden.
Sofern eine Tabelle für eine SessionFactory nicht vorhanden ist, wird das
Mapping ignoriert. Enthält eine Datenbank keine Tabellen werden alle Mappings
ignoriert. Wird jedoch auf eine Entität zugegriffen, die nicht in der Daten-
bank vorkomment, wird von NHibernate eine Exception geworfen.