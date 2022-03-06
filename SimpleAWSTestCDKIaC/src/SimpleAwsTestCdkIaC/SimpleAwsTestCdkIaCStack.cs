using System.Linq;
using Amazon.CDK;
using Constructs;
using Amazon.CDK.AWS.EC2;

namespace SimpleAwsTestCdkIaC
{
    public class SimpleAwsTestCdkIaCStack : Stack
    {
        
        /*
         * I'm pretty sure this is the wrong way to go about using the CDK, but if I use the likes of VPC and Subnet, I
         * get extra stuff I don't want - internet gateways, route tables, and so on.
         * If I use the Cfn-prefixed methods, I get what I ask for and nothing more.  Why would this be better than
         * using Cloudformation?
         */
        internal SimpleAwsTestCdkIaCStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here
            //var vpc = new Vpc(this, "SimpleAWSTestVPC", new VpcProps
            //{
            //    Cidr = "10.0.0.0/24" //,
                //SubnetConfiguration = new [] {new SubnetConfiguration() {CidrMask = 26, SubnetType = SubnetType.PUBLIC, MapPublicIpOnLaunch = true, Name ="Subnet1"}}
            //});

            var vpc = new CfnVPC(this, "SimpleAWSTestVPC", new CfnVPCProps() { CidrBlock = "10.0.0.0/24" });
            
            //var sub1 = new Subnet(this, "SimpleAWSTestPublicSubnet",
            //    new SubnetProps() { VpcId = vpc.Ref, AvailabilityZone = "eu-west-2a", CidrBlock = "10.0.0.0/26", MapPublicIpOnLaunch = true });
            
            var sub1 = new CfnSubnet(this, "SimpleAWSTestPublicSubnet1",
                new CfnSubnetProps()
                {
                    CidrBlock = "10.0.0.0/26",
                    AvailabilityZone = "eu-west-2a",
                    MapPublicIpOnLaunch = true,
                    VpcId = vpc.Ref
                });
            var sub2 = new CfnSubnet(this, "SimpleAWSTestPublicSubnet2",
                new CfnSubnetProps()
                {
                    CidrBlock = "10.0.0.64/26",
                    AvailabilityZone = "eu-west-2b",
                    MapPublicIpOnLaunch = true,
                    VpcId = vpc.Ref
                });

            var igw = new CfnInternetGateway(this, "SimpleAWSTestIGW", new CfnInternetGatewayProps());
            var igwassign = new CfnVPCGatewayAttachment(this, "SimpleAWSTestVPCIgwAttach",
                new CfnVPCGatewayAttachmentProps { InternetGatewayId = igw.AttrInternetGatewayId, VpcId = vpc.Ref });
            var routeTable = new CfnRouteTable(this, "SimpleAWSTestRouteTable",
                new CfnRouteTableProps() { VpcId = vpc.Ref });
            var route = new CfnRoute(this, "SimpleAWSTestPublicSubnetRoute",
                new CfnRouteProps()
                {
                    RouteTableId = routeTable.AttrRouteTableId,
                    GatewayId = igw.AttrInternetGatewayId, DestinationCidrBlock = "0.0.0.0/0"
                });
            var subnetroutetableassoc1 = new CfnSubnetRouteTableAssociation(this, "SubnetRouteTableAssoc1",
                new CfnSubnetRouteTableAssociationProps()
                {
                    RouteTableId = routeTable.AttrRouteTableId, SubnetId = sub1.Ref
                });
            var subnetroutetableassoc2 = new CfnSubnetRouteTableAssociation(this, "SubnetRouteTableAssoc2",
                new CfnSubnetRouteTableAssociationProps()
                {
                    RouteTableId = routeTable.AttrRouteTableId, SubnetId = sub2.Ref
                });
            /*
            var sg = new CfnSecurityGroup(this, "SimpleTestAWSIG",
                new CfnSecurityGroupProps() { GroupDescription = "SG for SimpleAWSTest", VpcId = vpc.Ref });
            sg.SecurityGroupIngress = new CfnSecurityGroupIngress(this, "EgressRules",
                new CfnSecurityGroupIngressProps()
                {
                    GroupId = sg.Ref,
                    ToPort = 22.0d,
                    FromPort = 22.0d,
                    CidrIp = "0.0.0.0/0",
                    IpProtocol = "tcp"
                });
                */
            /*
            var securityGroup = new SecurityGroup(this, "SimpleAWSTestSecGrp",
                new SecurityGroupProps()
                {
                    AllowAllOutbound = true,
                    Vpc = vpc.LogicalId,
                    Description = "Security Group for Instances",
                    SecurityGroupName = "SimpleAWSTestSecurityGrp"
                });
            securityGroup.AddIngressRule(Peer.Ipv4("0.0.0.0/0"),
                new Port(new PortProps()
                {
                    FromPort = 22, Protocol = Protocol.TCP, StringRepresentation = "0.0.0.0/0", ToPort = 22
                }));
              */  
        }
    }
}
