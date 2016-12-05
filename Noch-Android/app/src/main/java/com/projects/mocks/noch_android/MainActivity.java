package com.projects.mocks.noch_android;

import android.content.Intent;
import android.net.Uri;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Window;
import android.webkit.WebView;
import android.webkit.WebViewClient;

public class MainActivity extends AppCompatActivity
{

    private WebView webview;
    @Override
    protected void onCreate(Bundle savedInstanceState)
    {


        super.onCreate(savedInstanceState);
        this.requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_main);

        webview = (WebView)findViewById(R.id.webview);
        if(webview != null)
        {
            webview.loadUrl("https://noch.gear.host/Chat");
            webview.setWebViewClient(new WebViewClient(){
                public boolean shouldOverrideUrlLoading(WebView view, String url) {

                    String url2="https://noch.gear.host";
                    // all links  with in ur site will be open inside the webview
                    //links that start ur domain example(http://www.example.com/)
                    if (url != null && url.startsWith(url2)){
                        return false;
                    }
                    // all links that points outside the site will be open in a normal android browser
                    else  {
                        view.getContext().startActivity(
                                new Intent(Intent.ACTION_VIEW, Uri.parse(url)));
                        return true;
                    }
                }
            });
        }
    }
}
