
outFile = open('../Notes/Z80/z80.xml', 'w')  
outFile.write('<Cpu_z80>\n')

inFile = open('../Notes/Z80/disz80.txt', 'r') 
 
prebyte = '' 
for line in inFile:
     
	arr = line.strip().split('\t')

	# print (str(arr) + ' ' + str(len(arr)))

	operand = ''
	if (len(arr) == 3 and arr[2] != ""):
		operand = ' Operands="' + arr[2] + '"';

	if (arr[0][0] == '#'):
		prebyte = arr[0][1:]
		print ('-----> ' + prebyte)
	elif (len(arr) == 1):
		pass
	else:
		if (arr[1][0] == '#'):
			pass
			outFile.write('\t<OpCode Op="' + prebyte + arr[0] + '" Table="' + arr[1][1:] + '" />\n')
		else:
			outFile.write('\t<OpCode Op="' + prebyte + arr[0] + '" Mnem="' + arr[1] + '"' + operand + ' />\n')

inFile.close()

outFile.write('</Cpu_z80>\n')
outFile.close()