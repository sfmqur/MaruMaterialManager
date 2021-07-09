import MySQLdb as sql

db_name = 'finances'
table_name = 'transaction'
file_path = './finances/transaction.csv'
delimiter = ','

db = sql.connect(user='root', passwd='sqlzubera42', db=db_name)
c=db.cursor()

c.execute("DESCRIBE %s" %table_name)

#get column headers
b = c.fetchall()
cols = []
for i in range(len(b)):
	cols.append(b[i][0])
#print(cols)

c.execute("SELECT * FROM %s" %table_name)
rows = c.fetchall()

file = open(file_path, 'w')

out = ""
for i in range(len(cols)):
	if i == len(cols)-1:
		out += '%s\n' %cols[i]
	else:
		out += '%s%s' %(cols[i],delimiter)

for i in range(len(rows)):
	for j in range(len(cols)):
		if j == len(cols)-1:
			out += '%s\n' %str(rows[i][j])
		else:
			out += '%s%s' %(rows[i][j],delimiter)
#print(out)
file.write(out)
file.close()
c.close()
db.close()