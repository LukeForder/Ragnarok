create table Product(
		Id bigint identity(1,1) not null primary key
	,	Description varchar(100) not null
)

create table PurchaseOrder (
		Id bigint identity(1,1) not null primary key
	,	Reference varchar(100) not null 
)

create table PurchaseOrderItem (
		Id			bigint identity(1,1) not null primary key
	,	Quantity	int not null
	,	Price		decimal(18, 2) not null
	,	ProductId	bigint not null
	,	PurchaseOrderId bigint not null
	,	constraint FK_PurchaseOrder_PurchaseOrderItem
		foreign key (PurchaseOrderId)
		references PurchaseOrder(Id)
)