## Simple AWS Test of API and Parameters and AWS CodePipeline ##

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

There's an Infrastructure folder in the project that builds all the requirements for the 
project itself.  In this root folder, there's build artefacts required for deployment to EC2.
There is also a Code Pipeline infrastructure json template.
You can use the template like this:

    aws cloudformation create-stack --stack-name SimpleAWSTestCodePipeline --template-body file://../../CodePipelineInfrastructure.json --parameters ParameterKey=RolesStack,ParameterValue=SimpleAWSTestRolesandPolicies ParameterKey=EC2Stack,ParameterValue=SimpleAWSTestEC2Stack --on-failure DO_NOTHING

The only hard-coded thing, at the moment, is the ARN to my GitHub connection, and the name of the repository and the branch.
Otherwise, it should be fairly complete.  The artefact S3 bucket is created in the Policies and Roles stack.


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
