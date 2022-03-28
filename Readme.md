## Simple AWS Test of API and Parameters ##

This has grown from a simple test of loading parameters from AWS Parameter Store, to a full blown test of
building infrastructure using CloudFormation, and using CodeBuild, CodeDeploy and CodePipeline to do
CI/CD on AWS to EC2 instances.

In this root directory, you'll find this Readme.md file, as well as some other bits and bobs required:

- the appspec.yml and buildspec.yml files required by CodeDeploy and CodeBuild respectively,
- a scripts directory, containing deploy and run scripts for the simple .NET 5 API in the main project.  This runs the application using a systemd service,
- website.service is a file for setting up the .NET 5 application as a systemd service,
- The SimpleAWSTest5.0API and SimpleAWSTest5.0Test projects, of which we only use the API project at the moment.

The SimpleAWSTest5.0API project contains the usual stuff required for a .NET 5 ASP.NET application, together with an Infrastructure directory containing the CloudFormation files.
There is a Read Me file here to tell you what to do.

This is not complete, at present.  Things to do include:

- Move and complete the CodePipelineInfrastructure.json file, so we can set up the codepipeline easily.
- Write CloudFormation templates for all Policies and Roles we use, together with creating the artefact S3 bucket and stuff.
- Fix some issues surround the SecurityGroup and TargetGroup rules - I don't think the EC2 instances need to be so public, so the instances can allow traffic from the Load Balancer only (or at least the CIDR block for the VPC).  This means the Load Balancer will need a Security Group for external access.  Of course, we'll still need ssh access to the instances.

## CodeBuild local instructions. ##

You'll need a couple of things for CodeBuild local.  Download the standard image sources here:

    git clone https://github.com/aws/aws-codebuild-docker-images.git

and then build the latest one (the standard 5.0 has .net 5.0 but not .net 6.0 yet.)

    sudo docker build -t aws/codebuild/standard:5.0 .

I used sudo here as it wouldn't build if I didn't.  It takes an absolute age to build though.

Then you'll need the local image too:

    docker pull amazon/aws-codebuild-local:latest

Use something like

    ./codebuild_build.sh -i aws/codebuild/standard:5.0 -a ~/work/artefacts -s .

to use the AWS CodeBuild locally.  Here, -i says which codebuild standard image to use - 5.0 does .net 5.0 but not 6.0.  -a says where to put the artefacts created. -s is the source.
