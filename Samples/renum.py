import os

files = os.listdir()

count = 0
backwards = []
renum = -1
numbers = {}
names = {}
existing = {}

for file in files:
	if file.endswith('.cs'):
		# --------------------------
		backwards.insert(0, file)
		# --------------------------
		existing[file] = 1
		# --------------------------
		arr = file.split('-')
		# --------------------------
		if (len(arr) != 2):
			raise Exception('BAD: ' + str(len(arr)))
		# --------------------------
		if (arr[0] in numbers):
			raise Exception('BAD: ' + arr[0])
		else:
			numbers[arr[0]] = arr[1]
		# --------------------------
		if (arr[1] in names):
			raise Exception('BAD: ' + arr[1])
		else:
			names[arr[1]] = arr[0]
		# --------------------------
		count += 1

renum = (count*10)-10 + 1000

for file in backwards:
	# --------------------------
	arr = file.split('-')
	# --------------------------
	new_name = str(renum) + '-' + arr[1]
	# --------------------------
	if (new_name in existing):
		pass
	else: 
		os.rename(arr[0] + '-' + arr[1], new_name)
		pass
	# --------------------------
	renum -= 10
