-- Fix the following Cassandra DDL, so that all queries from the files below can be executed without changes
 -- 02-upsert-readonly.sql and
 -- 03-query-readonly.sql

-- Create keyspace (press TAB for autocomplete in cqlsh)
CREATE KEYSPACE homework
WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 2 };

-- Use keyspace
USE homework;

-- Fix the following table creation script so that the subsequent queries work as expected
-- The table contains both the invoice header and the invoice detail fields
CREATE table invoice
(
  -- header fields
  invoice_id text,
  invoice_date date STATIC,
  invoice_address text STATIC,
  -- detail fields
  line_id int,
  article_name text,
  article_price decimal,
  PRIMARY KEY(invoice_id, article_name)
);

CREATE INDEX IF NOT EXISTS invoice_article_name
ON invoice(invoice_date);
