## Instructions for Deployment. ##

Use something like this:

    aws cloudformation create-stack --stack-name SimpleAWSTestVPCStack --template-body file://./VPCInfrastructure.json --on-failure DO_NOTHING

    aws cloudformation create-stack --stack-name SimpleAWSTestSGStack --template-body file://./SGConfigurationDeploymentInfrastructure.json --parameters ParameterKey=VPCStack,ParameterValue=SimpleAWSTestVPCStack --on-failure DO_NOTHING

    aws cloudformation create-stack --stack-name SimpleAWSTestEC2Stack --template-body file://./EC2DeployInfrastructure.json --parameters ParameterKey=VPCStack,ParameterValue=SimpleAWSTestVPCStack --on-failure DO_NOTHING

The first stack creates a VPC and two public subnets in eu-west-2 (hard-coded some of the availability zones unfortunately.)

The second stack creates a security group and a launch configuration.  This is done in
a separate stack becuase I couldn't get the subsequent auto-scaling group to see the
launch configuration.

The final stack creates an application load balancer, a target group and listener, and an
autoscaling group.  At the moment, this doesn't quite work as there is no link between
the ALB and the target group, I don't think.

This is all so we can use CodeDeploy to deploy the SimpleAWSTest application.

As I have already pointed out, there is no obvious dependence between the second and
third stack, but there is because of ordering.  I'm not sure how to make that explicit.

So, the main three files in here are:

  - VPCInfrastructure.json
  - SGConfigurationDeploymentInfrastructure.json
  - EC2DeployInfrastructure.json

Some other files in this folder are:

  - clicmd.txt contains some AWS CLI commands I've used to create things,
  - CodeDeployServiceRole.yml and PoliciesAndRoles.txt was an attempt to document some policies and roles required for CodeDeploy/CodeBuild
  - LoadBalancedEC2Instances.yml was an aborted attempt at CloudFormation in YAML,
  - userdata.txt contains some commands to install .NET 5.0 and the CodeDeploy agent on an instance.  This could have been used in the LaunchConfiguration, but in the end I created a new AIM.

This is the current state of things, as of 11th March 2022.

