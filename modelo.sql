create table cliente(    
    Nome nvarchar(100),
    CPF nvarchar(14) primary key not null,
    UF char(2),
    Celular varchar(15)
)


create table financiamento(
    Id int not null primary key identity(1,1),  
    CPF nvarchar(14),
    Tipo nvarchar(100),
    Total float,
    UltimoVencimento datetime2,
    constraint fk_financiamento_cliente foreign key (CPF) references cliente(CPF)
)

create table parcelas(
    Id int not null primary key identity(1,1),
    IdFinanciamento int not null,
    Numero int,
    Valor float,
    Vencimento datetime2,
    Pagamento datetime2,
    constraint fk_parcelas_financiamento foreign key (IdFinanciamento) references financiamento(Id)
)


-- Listar todos os clientes do estado de SP que possuem mais de 60% das parcelas pagas

select t.nome, t.CPF ,t.UF ,t.Celular from (
select 
	c.*	,
	count(p.id) as pagas,
	0 as total
from 
	cliente c
	inner join financiamento f  on f.CPF =c.CPF 
	inner join parcelas p on p.IdFinanciamento  = f.Id and not p.Pagamento is null	 
group by c.nome, c.CPF ,c.UF ,c.Celular 

UNION ALL

select 
	c.*	,
	0 as pagas,
	count(p.id) as total
from 
	cliente c
	inner join financiamento f  on f.CPF =c.CPF 
	inner join parcelas p on p.IdFinanciamento  = f.Id	 
group by c.nome, c.CPF ,c.UF ,c.Celular

) as t
where t.UF='SP'
group by t.nome, t.CPF ,t.UF ,t.Celular
having  ((CAST(sum(t.pagas) as float)/cast(sum(t.total) as float))*100) >=60.0


--Listar os primeiros quatro clientes que possuem alguma parcela com mais de cinco dias em atraso (Data Vencimento maior que data atual E data pagamento nula)
select 
	top 4	
	c.*	
from 
	cliente c
	inner join financiamento f  on f.CPF =c.CPF 
	inner join parcelas p on p.IdFinanciamento  = f.Id	 
where DATEADD(day,5,p.Vencimento) < GETDATE() and p.Pagamento is null 
group by c.nome, c.CPF ,c.UF ,c.Celular 