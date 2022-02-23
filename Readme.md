## Simple AWS Test of API and Parameters ##

The test here is several-fold:

    Firstly, testing CodeBuild, CodeDeploy and CodePipeline from a github repo.
    Secondly, using Parameter Store to configure the simple application.

I guess this'll be deployed eventually to an EC2 instance.  It only needs to be simple.

Use something like

    ./codebuild_build.sh -i aws/codebuild/standard:5.0 -a ~/work/artefacts -s .

to use the AWS CodeBuild locally.  Here, -i says which codebuild standard image to use - 5.0 does .net 5.0 but not 6.0.  -a says where to put the artefacts created. -s is the source.
