import MySQLdb as sql

# data in should have tab delimited data with the first row being tab delimited column names in the table. 

db_name = 'finances'
table_name = 'transaction'
file_path = './finances/data.txt'
delimiter = ','

db = sql.connect(user='root', passwd='sqlzubera42', db=db_name)
c=db.cursor()

file = open(file_path, 'r')
rows = []

for l in file:
	rows.append(l.strip().split(delimiter))
file.close()

num_cols = len(rows[0])

#build the query
trans = 'INSERT INTO %s (' %(table_name)
for i in range(num_cols):
	if i == num_cols-1:
		trans += '%s) ' %(rows[0][i])
	else:
		trans += '%s, ' %(rows[0][i])
trans += 'VALUES ('

for i in range(1,len(rows)):
	new_trans = trans
	for j in range(num_cols):
		if j == num_cols-1:
			new_trans += "'%s')" %(rows[i][j])
		else:
			new_trans += "'%s', " %(rows[i][j])
	print(new_trans+'\n')
	c.execute(new_trans)

db.commit()
c.close()
db.close()