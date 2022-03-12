# U Rush
Let's rush to the future! 
## Development Guideline
(Updated on 2022/03/12)
- **DO NOT** develop on the `main` branch. 
- Create your own branch named as `dev-{your_name}`. 
  - Avoid two persons working concurrently on the same branch. 
- After finishing a stage of development: 
  - Fetch `origin`;
  - Merge `main` to your own  `dev-{your_name}`;
  - Resolve any conflictions and make sure your code works correctly;
  - **Inform other team members** that you are going to merge to `main` and wait for their acknowledgment;
  - With others' agreement, merge  `dev-{your_name}` to `main`;
  - Push `origin`;
  - Notify your team members that the merge has been completed. 
- After receiving the notification that someone has updated `main`: 
  - Fetch `origin`;
  - Merge `main` to your own  `dev-{your_name}`;
  - Resolve any conflictions and make sure your code works correctly;
  - Continue development. 
