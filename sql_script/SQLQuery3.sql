select * from dbo.Supply

insert into supply(Subject,CoalType,Category,UserID,CreatedOn,ValidDate,Region,
Description,WholesaleDes,ShipDes,Volatility,GrainSize,AshContent,SurfurContent,
WaterContent,CalorificPower)
values('��Ӧʯ��ר��/ ����ú ��������ú',1,1,1,getdate(),'2009-12-31',1,
'��������ú���ϲ���ɽ�����ʰ�úΪԭ�ϣ�����ѡ�飬�ۣ�ɸ�ȹ��ռӹ��������ձ����˫�㣬������ٹ��˲��ϡ�ʵ����һ�����ԣ����Լ��Եľ��������������õıȱ����������ָ����ﵽ���貿��CJ/T44-1999����׼��',
'��������ϵ','��������ϵ',1,1,1,1,1,6000)
