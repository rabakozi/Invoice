-- All statements from this file should be executed without changes and without errors.

-- I recommend to watch the content of the invoice table after each command by running the statement
SELECT * FROM homework.invoice;

-- Switch to the keyspace homework;
USE homework;

-- Insert an invoice without any articles
INSERT INTO invoice (invoice_id, invoice_date, invoice_address)
VALUES ('INV-01', '2017-05-20', 'EPAM Budapest');

-- Insert some invoice lines without header information
INSERT INTO invoice (invoice_id, line_id, article_name, article_price)
VALUES ('INV-01', 1, 'Pencil', 0.89);
INSERT INTO invoice (invoice_id, line_id, article_name, article_price)
VALUES ('INV-01', 2, 'Notebook', 3.99);
INSERT INTO invoice (invoice_id, line_id, article_name, article_price)
VALUES ('INV-01', 3, 'Zenbook', 321.99);
INSERT INTO invoice (invoice_id, line_id, article_name, article_price)
VALUES ('INV-01', 4, 'Apple iPad', 128.99);

INSERT INTO invoice (invoice_id, line_id, article_name, article_price)
VALUES ('INV-02', 1, 'Kindle', 79.99);

INSERT INTO invoice (invoice_id, line_id, article_name, article_price)
VALUES ('INV-03', 1, 'Macbook Pro', 999.99);

-- Set invoice header information
UPDATE invoice
SET invoice_date = '2017-05-21', invoice_address = 'EPAM Budapest'
WHERE invoice_id = 'INV-02';

-- Set invoice header information
INSERT INTO invoice (invoice_id, invoice_date, invoice_address)
VALUES ('INV-03', '2017-05-23', 'EPAM Szeged');

-- Set invoice header information for a missing invoice
UPDATE invoice
SET invoice_date = '2017-05-23', invoice_address = 'EPAM Honolulu'
WHERE invoice_id = 'INV-04';
