-- All statements from this file should be executed without changes and without errors.
USE homework;

-- Select all articles of INV-02
SELECT line_id, article_name, article_price
FROM invoice
WHERE invoice_id = 'INV-02';

-- Select all articles of INV-01, articles should be sorted by name
SELECT line_id, article_name, article_price
FROM invoice
WHERE invoice_id = 'INV-01'
ORDER BY article_name;

-- Select all invoices issued on a specific date
SELECT invoice_id, invoice_address, line_id
FROM invoice
WHERE invoice_date = '2017-05-23';
