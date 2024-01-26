export interface LoginRequest {
    email: string,
    password: string
}

export interface LoginResponse {
    token: string,
    currentUser: CurrentUser
}

export interface RegisterRequest {
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    birthDate: Date
}

export interface RegisterResponse {
    token: string,
    currentUser: CurrentUser
}

export interface CurrentUser {
    id: string,
    firstName: string,
    lastName: string,
    profilePhoto: string,
    roles: [Role]
}

export interface Role {
    name: string,
    description: string
}

export type Post = {
    id: string;
    firstName: string;
    lastName: string;
    userPhoto: string;
    title: string;
    body: string;
    createdOn: string;
    userId: string;
    numberOfLikes: number;
    numberOfComments: number;
    isLikedByCurrentUser: boolean;
    comments: Comments;
}

export type Posts = Array<Post>;

export type NewPost = {
    title: string;
    body: string;
}

export type PostFormProps = {
    onSubmit: (post: Post) => void;
}

export type Comment = {
    id: string;
    userId: string;
    firstName: string;
    lastName: string;
    userPhoto: string;
    createdOn: string;
    body: string;
    isCurrentUserComment: boolean;
}

export type Comments = Array<Comment>;

export type PostsListProps = {
    posts: Posts;
    onRemoveItem: (id: string) => void;
}

export type PostProps = {
    item: Post;
    onRemoveItem: (id: string) => void;
}

export type CommentProps = {
    item: Comment;
    onRemoveItem: (id: string) => void;
}

export type CommentFormProps = {
    postId: string;
    onSubmit: (comment: Comment) => void;
}

export type PostsState = {
    data: Posts;
    pageNumber: number;
    isLoading: boolean;
    isError: boolean;
}

interface PostsFetchAction {
    type: 'POSTS_FETCH';
}

interface PostsFetchSuccessAction {
    type: 'POSTS_FETCH_SUCCESS';
    payload: Posts;
}

interface PostsFetchFailureAction {
    type: 'POSTS_FETCH_FAILURE';
}

interface NewPostAction {
    type: 'NEW_POST';
    payload: Post;
}

interface DeletePostAction {
    type: 'DELETE_POST';
    payload: string;
}

interface IncreasePageNumberAction {
    type: 'INCREASE_PAGE_NUMBER';
}

export type PostsAction = PostsFetchAction 
| PostsFetchSuccessAction 
| PostsFetchFailureAction 
| NewPostAction 
| DeletePostAction
| IncreasePageNumberAction;