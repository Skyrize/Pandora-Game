﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostManagerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected CrewUI crewUI = null;
    [SerializeField] protected Player player = null;
    [SerializeField] protected RectTransform PostPanelContent = null;
    [SerializeField] protected GameObject PostCardPrefab = null;
    [SerializeField] List<PostUI> postCards = new List<PostUI>();

    public void ExchangePost(Post first, Post second)
    {
        PostUI firstUI = postCards.Find((postUI) => postUI.CurrentPost == first);
        PostUI secondUI = postCards.Find((postUI) => postUI.CurrentPost == second);
        CrewMember firstEmployee = first.Employee;
        CrewMember secondEmployee = second.Employee;

        ClearPost(first);
        ClearPost(second);
        first.SetEmployee(secondEmployee);
        second.SetEmployee(firstEmployee);
        firstUI.UpdateUI(first);
        secondUI.UpdateUI(second);
        crewUI.UnselectCrewMember();
    }

    public void ClickPost(Post post)
    {
        Debug.Log("ClickPost");
        if (crewUI.CurrentCrewMember == null) {
            if (post.Employee != null) {
                Debug.Log("Select from post !");
                crewUI.SelectCrewMember(post.Employee);
                return;
            } else {
                Debug.Log("what");
                return;
            }
        }
        PostUI target = postCards.Find((postUI) => postUI.CurrentPost == post);
        Post currentPost = crewUI.CurrentCrewMember.currentPost;
        if (!crewUI.CurrentCrewMember)
            return;
        if (!post.Employee || post.Employee != crewUI.CurrentCrewMember) {
            if (currentPost != null && post.Employee) {
                ExchangePost(currentPost, post);
                return;
            }
            if (currentPost != null) {
                ClearPost(currentPost);
                Debug.Log("Clearing current crew post");
            }
            if (post.Employee) {
                post.ClearEmployee();
                Debug.Log("Clearing selected post");
            }
            post.SetEmployee(crewUI.CurrentCrewMember);
            Debug.Log("Assigning current crew to post");
        }
                Debug.Log("Update UI");
        target.UpdateUI(post);
        crewUI.UnselectCrewMember();
    }

    public void ClearPost(Post post)
    {
        PostUI target = postCards.Find((postUI) => postUI.CurrentPost == post);
        post.ClearEmployee();
        target.UpdateUI(post);
    }

    public void RemovePost(Post post)
    {
        PostUI target = postCards.Find((postUI) => postUI.CurrentPost == post);

        postCards.Remove(target);
        GameObject.Destroy(target.gameObject);
    }

    public void AddPost(Post post)
    {
        PostUI cardUI = GameObject.Instantiate(PostCardPrefab, PostPanelContent).GetComponent<PostUI>();

        cardUI.UpdateUI(post);
        cardUI.onSelect.AddListener(this.ClickPost);
        cardUI.onFree.AddListener(this.ClearPost);
        postCards.Add(cardUI);
    }

    public void ClearUI()
    {
        PostPanelContent.ClearChilds(); // TODO: easy but dirty. Maybe remove them along when adding to inventory
    }
    
    public void BuildUI()
    {
        AddPost(player.controlPost);
        AddPost(player.repairStation);
        foreach (Post post in player.weaponry.weapons)
        {
            AddPost(post);
        }
    }

    private void OnEnable() {
        ClearUI();
        BuildUI();
    }

    private void OnDisable() {
        ClearUI();
    }
}
