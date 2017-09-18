USE homework;

-- Fix the CQL so that each invoice appears only once
SELECT invoice_id, invoice_date, invoice_address FROM invoice;

-- Fix the CQL so that articles are sorted by article name in a descending order
SELECT line_id, article_name, article_price
FROM invoice
WHERE invoice_id = 'INV-01'
ORDER BY article_name;
