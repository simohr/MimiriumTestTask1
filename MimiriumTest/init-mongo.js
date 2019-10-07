use CompaniesDb
db.createCollection("CompaniesCounter")
db.CompaniesCounter.insertOne({"counter":0})