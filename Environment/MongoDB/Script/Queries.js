db.invoice.find({_id : 'INV-02'},{_id : 1, lines: 1}).pretty();
db.invoice.find({_id : 'INV-01'},{_id : 1, lines: 1}).pretty();
db.invoice.find({invoice_date : new Date('2017-05-23')},{_id : 1, lines: 1}).pretty();

db.invoice.find({'lines.article_name' : 'Kindle'}).explain()