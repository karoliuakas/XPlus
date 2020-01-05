from lxml import html
import requests

URL = 'url'
with open(URL) as f:
	URL = str(f.readline())
#URL = 'https://autogidas.lt/skelbimas/opel-omega-benzinasdujos--2000-m-universalas-0132673799.html'


page = requests.get(URL)
tree = html.fromstring(page.content)
tackiuList = tree.xpath('//a[@class="breadcrumb-item"]/@itemid')
kaina = tree.xpath('//div[@class="price"]/text()')
name = tree.xpath('//h1[@class="title"]/text()')
left = tree.xpath('//div[@class="left"]/text()')
right = tree.xpath('//div[@class="right"]/text()')
pic = tree.xpath('//img[@class="show"]/@src')
if not kaina:
	kaina = 'Sutartine'
else:
	kaina = kaina[0]

right.insert(0, kaina)
left = list(map(lambda x:x.strip(),left))
left = list(filter(None, left))
right = list(map(lambda x:x.strip(),right))
right = list(filter(None, right))

left = [x for x in left if x != 'Kaina eksportui']
left = [x for x in left if x != 'Telefonas']
left = [x for x in left if x != 'Pardavėjas']

myYear = left.index("Metai")
fy = int(right[myYear][0:4]) - 3
ly = int(right[myYear][0:4]) + 3
myYear = int(right[myYear][0:4])

if "Variklis" in left:
	myEngine = left.index("Variklis")
	if len(right[myEngine]) > 10:
		myEngine = int(right[myEngine][7:10])
else:
	myEngine = -1

if "Rida, km" in left:
	myMileage = left.index("Rida, km")
	dgt = str()
	for i in range(0, len(right[myMileage])):
		s = right[myMileage]
		if s[i].isdigit():
			dgt += str(s[i])
	myMileage = int(dgt)
else:
	myMileage = -1


tackiuList = str(tackiuList[-1])+ f"?f_41={fy}&f_42={ly}"

notTacke = False
if "automobiliai" not in tackiuList:
	notTacke = True

carList = requests.get(tackiuList)
tree2 = html.fromstring(carList.content)
pages = tree2.xpath('//div[@class="page "]/text() | //div[@class="page"]/text()')
if pages:
	lastPage = int(pages[-1])
else:
	lastPage = 1

kainosList = []
galiosList = []
ridosList = []
metuList = []
for i in range(1,lastPage+1):
	carList = requests.get(tackiuList + f"&page={i}")
	tree2 = html.fromstring(carList.content)
	kainosList += tree2.xpath('//div[@class="item-price"]/text()')
	galiosList += tree2.xpath('//p[@class="primary"]/text()')
	ridosList += tree2.xpath('//p[@class="secondary"]/text()')
	metuList += tree2.xpath('//p[@class="primary"]/text()')
kainosList = list(map(lambda x:x.strip(),kainosList)) # nutrinu tarpus
kainosList = list(filter(None, kainosList)) # nutrinu tuscius listo narius
kainosList = [w.replace(' ', '') for w in kainosList] # nutrinu tarpa jei kaina > 10k ?
kainosList = list(map(float,' '.join(kainosList).replace('€','').split())) # konvertuoju lista i float ir nutrinu eura
avgKaina = sum(kainosList)/len(kainosList)


swap = {'ą': 'a', 'č': 'c', 'ę': 'e', 'ė': 'e', 'į': 'i', 'š': 's', 'ų': 'u', 'ū': 'u', 'ž': 'z',
		   	'Ą': 'A', 'Č': 'C', 'Ę': 'E', 'Ė': 'E', 'Į': 'I', 'Š': 'S', 'Ų': 'U', 'Ū': 'U', 'Ž': 'Z',
		    '[': '', ']': '', ', ': ',', "','": "_", "'": "", 'Telefonas_': '', 'Kaina eksportui_': ''}
left = str(left)
right = str(right)
pic = str(pic)
for k, v in swap.items():
	left = left.replace(k,v)
	right = right.replace(k,v)
	pic = pic.replace(k,v)


#print(f"Automobilio skelbimo url: {URL} \n")

print(left)
print(right)

#for i,j in zip(left, right):
#    print(f"{i}: {j}")

pricerep = {' ': '', '€': ''}

for k, v in pricerep.items():
	kaina = kaina.replace(k,v)


print(int(avgKaina))
print(kaina)

noKaina = False
if not kaina.isdigit():
	noKaina = True
	print('Negalime palyginti kainos')
elif int(kaina) > avgKaina:
	print('Brangesne uz vidutine')
elif int(kaina) == avgKaina:
	print('Vidutine rinkos kaina')
else:
	print('Pigesne uz vidutine')
print(len(kainosList))

if notTacke == False and myMileage >= 0 and myEngine >=0 and noKaina == False:
	metuList = str(metuList)
	metuList = metuList.split(",")
	metuList = list(map(lambda x:x.strip(),metuList)) # nutrinu tarpus
	indices = [i for i, elem in enumerate(metuList) if ' m' in elem]
	year=0
	for i in range(0, len(indices)):
		dgt = str()
		ig = indices[i]
		for s in range(0, len(metuList[ig])):
			powah = metuList[ig]
			if powah[s].isdigit():
				dgt += str(powah[s])
		year += int(dgt)
	year = year//len(indices)
	
	
	galiosList = str(galiosList)
	galiosList = galiosList.split(",")
	galiosList = list(map(lambda x:x.strip(),galiosList)) # nutrinu tarpus
	indices = [i for i, elem in enumerate(galiosList) if ' kW' in elem]
	engine=0
	for i in range(0, len(indices)):
		dgt = str()
		ig = indices[i]
		for s in range(0, len(galiosList[ig])):
			powah = galiosList[ig]
			if powah[s].isdigit():
				dgt += str(powah[s])
		engine += int(dgt)
	engine = engine//len(indices)
	
	ridosList = str(ridosList)
	ridosList = ridosList.split(",")
	ridosList = list(map(lambda x:x.strip(),ridosList)) # nutrinu tarpus
	indices = [i for i, elem in enumerate(ridosList) if ' km' in elem]
	mileage=0
	for i in range(0, len(indices)):
		dgt = str()
		ig = indices[i]
		for s in range(0, len(ridosList[ig])):
			powah = ridosList[ig]
			if powah[s].isdigit():
				dgt += str(powah[s])
		mileage += int(dgt)
	mileage = mileage//len(indices)
	
	x = int(kaina)/myYear + int(kaina)/(myEngine*10) + int(kaina)/(myMileage/100)
	y = int(avgKaina)/year + int(avgKaina)/(engine*10) + int(avgKaina)/(mileage/100)
	print(round(y/x,2))
else:
	print('-1')

print(pic)