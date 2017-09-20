// Invoice seed 

use homework;

db.invoice.ensureIndex({'invoice_date':1});
db.invoice.ensureIndex({'article_name':1});

db.invoice.drop()
var invoiceColl = db.getCollection("invoice");

// Insert an invoice without any articles
invoiceColl.update(
    {_id : 'INV-01'},
    {
        $set: {
            invoice_date : new Date('2017-05-20'),
            invoice_address : 'EPAM Budapest'
        }
    },
    {
        upsert: true,
    }
)

//invoiceColl.update(
//    {_id : 'INV-01', 'lines.line_id': {$ne: NumberInt(1)}},
//    {
//        $push: { 
//            lines : {
//            line_id : NumberInt(1),
//                article_name : 'Pencil',
//                article_price : NumberDecimal(0.89)
//            }
//    }},
//    {
//        upsert: true,
//    }
//);

// Insert some invoice lines without header information
invoiceColl.update(
    {_id : 'INV-01'},
    {
    $addToSet: { 
            lines : {
            line_id : NumberInt(1),
                article_name : 'Pencil',
                article_price : NumberDecimal(0.89)
            }
    }},
    {
        upsert: true,
    }
);

invoiceColl.update(
    {_id : 'INV-01'},
    {
    $addToSet: { 
            lines : {
            line_id : NumberInt(2),
                article_name : 'Notebook',
                article_price : NumberDecimal(3.99)
            }
    }},
    {
        upsert: true,
    }
);

invoiceColl.update(
    {_id : 'INV-01'},
    {
    $addToSet: { 
            lines : {
            line_id : NumberInt(3),
                article_name : 'Zenbook',
                article_price : NumberDecimal(321.99)
            }
    }},
    {
        upsert: true,
    }
);

invoiceColl.update(
    {_id : 'INV-01'},
    {
    $addToSet: { 
            lines : {
            line_id : NumberInt(4),
                article_name : 'Apple iPad',
                article_price : NumberDecimal(128.99)
            }
    }},
    {
        upsert: true,
    }
);

invoiceColl.update(
    {_id : 'INV-02'},
    {
    $addToSet: { 
            lines : {
            line_id : NumberInt(1),
            article_name : 'Kindle',
            article_price : NumberDecimal(79.99)
            }
    }},
    {
        upsert: true,
    }
);

invoiceColl.update(
    {_id : 'INV-03'},
    {
    $addToSet: { 
            lines : {
            line_id : NumberInt(1),
            article_name : 'Macbook Pro',
            article_price : NumberDecimal(999.99)
            }
    }},
    {
        upsert: true,
    }
);

// Set invoice header information
invoiceColl.update(
    {_id : 'INV-02'},
    {
        $set: {
            invoice_date : new Date('2017-05-21'),
            invoice_address : 'EPAM Budapest'
        }
    },
    {
        upsert: true,
    }
)

// Set invoice header information
invoiceColl.update(
    {_id : 'INV-03'},
    {
        $set: {
        invoice_date : new Date('2017-05-23'),
            invoice_address : 'EPAM Szeged'
        }
    },
    {
        upsert: true,
    }
)

// Set invoice header information for a missing invoice
invoiceColl.update(
    {_id : 'INV-04'},
    {
        $set: {
        invoice_date : new Date('2017-05-23'),
            invoice_address : 'EPAM Honolulu'
        }
    },
    {
        upsert: true,
    }
)
