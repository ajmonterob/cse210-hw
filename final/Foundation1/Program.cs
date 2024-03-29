using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        // Create videos and add comments to them
        var videos = new List<Video>
        {
            new Video("How to travel to Paris#", "EurpTripAdvisor", 300),
            new Video("Understanding the universe", "ScieneToday", 250),
            new Video("Would won this year elecctions", "AmericanPolitics", 400)
        };

        // Adding comments to the first video
        videos[0].AddComment(new Comment("MaryStuart", "Great video, thanks!"));
        videos[0].AddComment(new Comment("SergioRamos", "Very helpful."));
        
        // Adding comments to the second video
        videos[1].AddComment(new Comment("NerdFan123", "Nice explanation."));
        
        // Adding comments to the third video
        videos[2].AddComment(new Comment("User3556", "Ellections are much  clear now."));
        videos[2].AddComment(new Comment("JohnBlake", "Looking forward to more videos."));

        // Display information for each video
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}, Author: {video.Author}, Length: {video.Length} seconds, Comments: {video.GetNumberOfComments()}");
            foreach (var comment in video.Comments)
            {
                Console.WriteLine($"\t{comment.Name}: {comment.Text}");
            }
            Console.WriteLine(); // Just for spacing
        }
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

public class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}
